using E_Commerce.Application.Common.Contracts.Payments.DTOs;

namespace E_Commerce.Application.Common.Contracts.Payments;


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