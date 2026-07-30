using E_Commerce.Domain.Features.PaymentFeature.Entities;

namespace E_Commerce.Domain.Features.PaymentFeature.Interfaces;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(
        Guid paymentId,
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
}