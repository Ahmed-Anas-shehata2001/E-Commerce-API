using E_Commerce.Domain.Features.Cart_Feature;
using E_Commerce.Domain.Features.CartFeature.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories;

public sealed class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cart?> GetByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        return await _context.Carts
            .Include(x => x.Items)
                .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(
                x => x.CustomerId == customerId,
                cancellationToken);
    }

    public async Task<Cart?> GetByIdAsync(
        Guid cartId,
        CancellationToken cancellationToken)
    {
        return await _context.Carts
            .Include(x => x.Items)
                .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(
                x => x.Id == cartId,
                cancellationToken);
    }

    public async Task AddAsync(
        Cart cart,
        CancellationToken cancellationToken)
    {
        await _context.Carts.AddAsync(
            cart,
            cancellationToken);
    }

    public void Remove(Cart cart)
    {
        _context.Carts.Remove(cart);
    }

    public Task<bool> ExistsAsync(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        return _context.Carts.AnyAsync(
            x => x.CustomerId == customerId,
            cancellationToken);
    }
}