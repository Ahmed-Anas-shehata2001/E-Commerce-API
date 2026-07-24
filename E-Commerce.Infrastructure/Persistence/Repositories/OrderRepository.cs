using E_Commerce.Domain.Features.OrderFeature.Entities;
using E_Commerce.Domain.Features.OrderFeature.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(
        Guid orderId,
        CancellationToken cancellationToken)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(
                x => x.Id == orderId,
                cancellationToken);
    }

    public async Task<Order?> GetByIdWithItemsAsync(
        Guid orderId,
        CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(x => x.Items)
            .FirstOrDefaultAsync(
                x => x.Id == orderId,
                cancellationToken);
    }

    public async Task<List<Order>> GetByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        return await _context.Orders
            .Include(x => x.Items)
            .Where(x => x.CustomerId == customerId)
            .OrderByDescending(x => x.OrderedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        Order order,
        CancellationToken cancellationToken)
    {
        await _context.Orders.AddAsync(
            order,
            cancellationToken);
    }

    public void Remove(Order order)
    {
        _context.Orders.Remove(order);
    }
}