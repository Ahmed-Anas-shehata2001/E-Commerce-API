using E_Commerce.Application.Common.Models;
using E_Commerce.Application.Features.Payment;
using E_Commerce.Domain.Features.PaymentFeature.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories;

public sealed class PaymentReadRepository : IPaymentReadRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentReadRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentDto?> GetPaymentByIdAsync(
        Guid paymentId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Payments
            .AsNoTracking()
            .Where(p => p.Id == paymentId)
            .Select(p => new PaymentDto(
                p.Id,
                p.OrderId,
                p.Amount,
                p.Status,
                p.PaymentMethod,
                p.Gateway,
                p.TransactionId,
                p.CreatedAtUtc,
                p.PaidAtUtc))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<PaymentDto>> GetOrderPaymentsAsync(
        Guid orderId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Payments
            .AsNoTracking()
            .Where(p => p.OrderId == orderId)
            .OrderByDescending(p => p.CreatedAtUtc)
            .Select(p => new PaymentDto(
                p.Id,
                p.OrderId,
                p.Amount,
                p.Status,
                p.PaymentMethod,
                p.Gateway,
                p.TransactionId,
                p.CreatedAtUtc,
                p.PaidAtUtc))
            .ToListAsync(cancellationToken);
    }

    public async Task<PagedResult<PaymentDto>> GetPaymentsAsync(
        GetPaymentsQuery query,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Domain.Features.PaymentFeature.Entities.Payment> payments =
            _context.Payments.AsNoTracking();

        if (query.Status.HasValue)
            payments = payments.Where(p => p.Status == query.Status);

        if (query.PaymentMethod.HasValue)
            payments = payments.Where(p => p.PaymentMethod == query.PaymentMethod);

        if (query.OrderId.HasValue)
            payments = payments.Where(p => p.OrderId == query.OrderId);

        if (query.CustomerId.HasValue)
        {
            payments = payments.Where(p =>
                p.Order.CustomerId == query.CustomerId);
        }

        var totalCount = await payments.CountAsync(cancellationToken);

        var items = await payments
            .OrderByDescending(p => p.CreatedAtUtc)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new PaymentDto(
                p.Id,
                p.OrderId,
                p.Amount,
                p.Status,
                p.PaymentMethod,
                p.Gateway,
                p.TransactionId,
                p.CreatedAtUtc,
                p.PaidAtUtc))
            .ToListAsync(cancellationToken);

        return new PagedResult<PaymentDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }



    public async Task<PagedResult<PaymentDto>> GetCustomerPaymentsAsync(
    Guid customerId,
    GetMyPaymentsQuery query,
    CancellationToken cancellationToken)
    {
        IQueryable<Payment> payments = _context.Payments
            .AsNoTracking()
            .Include(p => p.Order)
            .Where(p => p.Order.CustomerId == customerId);

        if (query.Status.HasValue)
            payments = payments.Where(p => p.Status == query.Status);

        if (query.PaymentMethod.HasValue)
            payments = payments.Where(p => p.PaymentMethod == query.PaymentMethod);

        var totalCount = await payments.CountAsync(cancellationToken);

        var items = await payments
            .OrderByDescending(p => p.CreatedAtUtc)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new PaymentDto(
                p.Id,
                p.OrderId,
                p.Amount,
                p.Status,
                p.PaymentMethod,
                p.Gateway,
                p.TransactionId,
                p.CreatedAtUtc,
                p.PaidAtUtc))
            .ToListAsync(cancellationToken);

        return new PagedResult<PaymentDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }

    public async Task<PaymentDto?> GetCustomerPaymentByIdAsync(
    Guid customerId,
    Guid paymentId,
    CancellationToken cancellationToken)
    {
        return await _context.Payments
            .AsNoTracking()
            .Include(p => p.Order)
            .Where(p =>
                p.Id == paymentId &&
                p.Order.CustomerId == customerId)
            .Select(p => new PaymentDto(
                p.Id,
                p.OrderId,
                p.Amount,
                p.Status,
                p.PaymentMethod,
                p.Gateway,
                p.TransactionId,
                p.CreatedAtUtc,
                p.PaidAtUtc))
            .FirstOrDefaultAsync(cancellationToken);
    }
}