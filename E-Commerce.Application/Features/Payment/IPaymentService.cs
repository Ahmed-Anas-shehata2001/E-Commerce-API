using E_Commerce.Application.Features.PaymentFeature.DTOs;

namespace E_Commerce.Application.Features.PaymentFeature.Interfaces;


// app workflow
public interface IPaymentService
{

    Task<CreatePaymentResult> CreatePaymentAsync(
        Guid orderId,
        CancellationToken cancellationToken = default);

    Task VerifyPaymentAsync(
        string transactionId,
        CancellationToken cancellationToken = default);

    Task RefundPaymentAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default);

    Task CancelPaymentAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default);
}