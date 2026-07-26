using E_Commerce.Domain.Features.PaymentFeature.Entities;

namespace E_Commerce.Domain.Features.PaymentFeature.Interfaces;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default);

    Task<Payment?> GetByOrderIdAsync(
        Guid orderId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Payment>> GetPaymentsByUserIdAsync(
        Guid customerId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Payment payment,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Payment payment,
        CancellationToken cancellationToken = default);



    Task<bool> ExistsByTransactionIdAsync(
    string transactionId,
    CancellationToken cancellationToken = default);

    Task<Payment?> GetByTransactionIdAsync(
        string transactionId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Payment>> GetPendingPaymentsAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Payment>> GetFailedPaymentsAsync(
        CancellationToken cancellationToken = default);
}