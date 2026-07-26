using E_Commerce.Domain.Features.PaymentFeature.Entities;
using E_Commerce.Domain.Features.PaymentFeature.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories;

public sealed class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Payment?> GetByIdAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(
                p => p.Id == paymentId,
                cancellationToken);
    }

    public async Task<Payment?> GetByOrderIdAsync(
        Guid orderId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(
                p => p.OrderId == orderId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Payment>> GetPaymentsByUserIdAsync(
        Guid customerId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Payments
            .Include(p => p.Order)
            .Where(p => p.Order.CustomerId == customerId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        Payment payment,
        CancellationToken cancellationToken = default)
    {
        await _context.Payments.AddAsync(payment, cancellationToken);
    }

    public Task UpdateAsync(
        Payment payment,
        CancellationToken cancellationToken = default)
    {
        _context.Payments.Update(payment);

        return Task.CompletedTask;
    }

    public async Task<bool> ExistsByTransactionIdAsync(
        string transactionId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Payments
            .AnyAsync(
                p => p.TransactionId == transactionId,
                cancellationToken);
    }

    public async Task<Payment?> GetByTransactionIdAsync(
        string transactionId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(
                p => p.TransactionId == transactionId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Payment>> GetPendingPaymentsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Payments
            .Where(p => p.Status == PaymentStatus.Pending)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Payment>> GetFailedPaymentsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Payments
            .Where(p => p.Status == PaymentStatus.Failed)
            .ToListAsync(cancellationToken);
    }
}