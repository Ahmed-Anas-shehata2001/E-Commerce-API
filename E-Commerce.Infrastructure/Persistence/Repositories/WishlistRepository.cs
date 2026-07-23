using E_Commerce.Domain.Features.WishlistFeature.Entities;
using E_Commerce.Domain.Features.WishlistFeature.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Persistence.Repositories;

public sealed class WishlistRepository : IWishlistRepository
{
    private readonly ApplicationDbContext _context;

    public WishlistRepository(ApplicationDbContext context)
    {
        _context = context;
    }



    public async Task<Wishlist?> GetByCustomerIdAsync(
    Guid customerId,
    CancellationToken cancellationToken)
    {
        return await _context.Wishlists
            .Include(x => x.Items)
                .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(
                x => x.CustomerId == customerId,
                cancellationToken);
    }




    public async Task<Wishlist?> GetByIdAsync(
    Guid wishlistId,
    CancellationToken cancellationToken)
    {
        return await _context.Wishlists
            .Include(x => x.Items)
                .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(
                x => x.Id == wishlistId,
                cancellationToken);
    }

    public async Task AddAsync(
        Wishlist wishlist,
        CancellationToken cancellationToken)
    {
        await _context.Wishlists.AddAsync(
            wishlist,
            cancellationToken);
    }

    public void Remove(Wishlist wishlist)
    {
        _context.Wishlists.Remove(wishlist);
    }

    public Task<bool> ExistsAsync(
        Guid customerId,
        CancellationToken cancellationToken)
    {
        return _context.Wishlists.AnyAsync(
            w => w.CustomerId == customerId,
            cancellationToken);
    }


    public async Task<int> GetWishlistCountAsync(
    Guid customerId,
    CancellationToken cancellationToken)
    {
        return await _context.WishlistItems
            .CountAsync(
                x => x.Wishlist.CustomerId == customerId,
                cancellationToken);
    }

    public async Task<bool> IsProductInWishlistAsync(
        Guid customerId,
        Guid productId,
        CancellationToken cancellationToken)
    {
        return await _context.WishlistItems
            .AnyAsync(
                x => x.Wishlist.CustomerId == customerId &&
                     x.ProductId == productId,
                cancellationToken);
    }
}