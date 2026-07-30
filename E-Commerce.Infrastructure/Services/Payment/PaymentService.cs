using E_Commerce.Application.Common.Contracts.Email;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Payments;
using E_Commerce.Application.Common.Contracts.Payments.DTOs;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Features.OrderFeature.Interfaces;
using E_Commerce.Domain.Features.PaymentFeature.Entities;
using E_Commerce.Domain.Features.PaymentFeature.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace E_Commerce.Infrastructure.Services.Payment;


/*
CreatePayment
│
├── Load Order
├── Validate Order
├── Create Payment
├── Save Payment
├── Call Gateway
├── Update Payment
└── Return Checkout Info
VerifyPayment
│
├── Verify Gateway Result
├── Load Payment
├── Mark Paid / Failed
├── Update Order
└── Send Email
RefundPayment
│
├── Load Payment
├── Validate
├── Call Gateway
├── Mark Refunded
└── Send Email
CancelPayment
│
├── Load Payment
├── Validate
├── Mark Cancelled
└── Save
 */
public sealed class PaymentService : IPaymentService
{
    private readonly IOrderRepository _orders;
    private readonly IPaymentRepository _payments;
    private readonly IPaymentGateway _gateway;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IEmailSender _emailSender;
    private readonly ILogger<PaymentService> _logger;
    private readonly PaymobOptions _paymobOptions;

    public PaymentService(
            IOrderRepository orders,
            IPaymentRepository payments,
            IPaymentGateway gateway,
            IUnitOfWork unitOfWork,
            IUserService userService,
            IEmailSender emailSender,
            ILogger<PaymentService> logger,
            IOptions<PaymobOptions> options)
    {
        _orders = orders;
        _payments = payments;
        _gateway = gateway;
        _unitOfWork = unitOfWork;
        _userService = userService;
        _emailSender = emailSender;
        _logger = logger;
        _paymobOptions = options.Value;
    }

    public async Task<CreatePaymentResult> CreatePaymentAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await _orders.GetByIdWithItemsAsync(orderId, cancellationToken);

        if (order is null)
            throw new Exception("Order not found.");

        if (order.IsPaid)
            throw new Exception("Already paid.");

        var amount = order.CalculateTotalPrice();

        var payment = new Domain.Features.PaymentFeature.Entities.Payment(
            order.Id,
            amount,
            PaymentMethod.Paymob,
            "Paymob");

        await _payments.AddAsync(payment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // prepare customer details
        string customerEmail = string.Empty;
        string customerName = string.Empty;


        try
        {
            var userResult = await _userService.GetUserByIdAsync(order.CustomerId, cancellationToken);

            if (userResult.IsSuccess)
            {
                var user = userResult.Value;



                customerEmail = user.Email ?? string.Empty;
                customerName = user.UserName ?? string.Empty;
            }



        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Unable to load customer information.");
        }

        var gatewayResponse = await _gateway.CreatePaymentAsync(
                new CreatePaymentRequest(
                    payment.Id,
                    payment.Amount,
                    _paymobOptions.Currency,
                    customerEmail,
                    customerName,
                    $"Payment for order {order.Id}"),
                cancellationToken);

        if (!gatewayResponse.Success)
        {
            payment.MarkAsFailed();
            await _payments.UpdateAsync(payment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            throw new Exception(gatewayResponse.ErrorMessage ?? "Payment gateway error.");
        }

        payment.MarkAsPending(gatewayResponse.TransactionId ?? string.Empty, gatewayResponse.PaymentIntentId ?? string.Empty);

        await _payments.UpdateAsync(payment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreatePaymentResult(payment.Id, gatewayResponse.CheckoutUrl ?? string.Empty);
    }

    public async Task CancelPaymentAsync(Guid paymentId, CancellationToken cancellationToken = default)
    {
        var payment = await _payments.GetByIdAsync(paymentId, cancellationToken);
        if (payment is null)
            throw new KeyNotFoundException("Payment not found.");

        payment.MarkAsCancelled();

        await _payments.UpdateAsync(payment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // best-effort notification
        try
        {
            var order = await _orders.GetByIdAsync(payment.OrderId, cancellationToken);

            if (order is not null)
            {
                await SendPaymentEmailAsync(
                    order.CustomerId,
                    "Payment cancelled",
                    $"Your payment {payment.Id} has been cancelled.",
                    cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send payment cancelled email.");
        }


    }


    public async Task RefundPaymentAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default)
    {
        var payment = await _payments.GetByIdAsync(paymentId, cancellationToken);

        if (payment is null)
            throw new KeyNotFoundException("Payment not found.");

        if (string.IsNullOrWhiteSpace(payment.TransactionId))
            throw new InvalidOperationException("Payment has no transaction id.");

        if (payment.Status != PaymentStatus.Paid)
            throw new InvalidOperationException(
                "Only paid payments can be refunded.");

        if (payment.Status == PaymentStatus.Refunded)
            throw new InvalidOperationException("Payment already refunded.");

        await _gateway.RefundPaymentAsync(
            new RefundPaymentRequest(
                payment.TransactionId,
                payment.Amount),
            cancellationToken);

        payment.MarkAsRefunded();

        await _payments.UpdateAsync(payment, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        try
        {
            var order = await _orders.GetByIdAsync(
                payment.OrderId,
                cancellationToken);

            if (order is not null)
            {
                await SendPaymentEmailAsync(
                    order.CustomerId,
                    "Payment refunded",
                    $"Your payment for order {order.Id} has been refunded successfully.",
                    cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send payment refunded email.");
        }
    }
    public async Task VerifyPaymentAsync(
        string transactionId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(transactionId))
            throw new ArgumentException("TransactionId is required.", nameof(transactionId));

        // Verify payment with Paymob
        var verifyResponse = await _gateway.VerifyPaymentAsync(
            new VerifyPaymentRequest(transactionId),
            cancellationToken);

        if (!verifyResponse.Success)
            throw new InvalidOperationException(
                verifyResponse.ErrorMessage ?? "Payment verification failed.");

        // Load payment
        var payment = await _payments.GetByTransactionIdAsync(
            transactionId,
            cancellationToken);

        if (payment is null)
            throw new KeyNotFoundException("Payment not found.");

        // Load order
        var order = await _orders.GetByIdWithItemsAsync(
            payment.OrderId,
            cancellationToken);

        if (order is null)
            throw new KeyNotFoundException("Order not found.");

        if (verifyResponse.IsPaid)
        {
            // Prevent duplicate processing
            if (payment.Status != PaymentStatus.Paid)
            {
                payment.MarkAsPaid(
                    verifyResponse.TransactionId ?? string.Empty,
                    verifyResponse.PaymentIntentId ?? string.Empty);

                await _payments.UpdateAsync(payment, cancellationToken);

                if (!order.IsPaid)
                {
                    order.MarkAsPaid();
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            try
            {
                await SendPaymentEmailAsync(
                    order.CustomerId,
                    "Payment successful",
                    $"Your payment for order {order.Id} has been received successfully.",
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send payment success email.");
            }
        }
        else
        {
            payment.MarkAsFailed();

            await _payments.UpdateAsync(payment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            try
            {
                await SendPaymentEmailAsync(
                    order.CustomerId,
                    "Payment failed",
                    $"Your payment for order {order.Id} could not be completed.",
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send payment failed email.");
            }
        }
    }



    // **************************** helpers *******************************// 

    private async Task SendPaymentEmailAsync(
    Guid customerId,
    string subject,
    string body,
    CancellationToken cancellationToken)
    {
        var userResult = await _userService.GetUserByIdAsync(customerId, cancellationToken);

        if (!userResult.IsSuccess)
            return;

        await _emailSender.SendAsync(
            userResult.Value.Email,
            subject,
            body,
            cancellationToken);
    }
}

