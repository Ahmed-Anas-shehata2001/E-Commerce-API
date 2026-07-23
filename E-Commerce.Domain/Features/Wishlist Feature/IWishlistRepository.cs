using E_Commerce.Domain.Features.WishlistFeature.Entities;

namespace E_Commerce.Domain.Features.WishlistFeature.Interfaces;

public interface IWishlistRepository
{
    Task<Wishlist?> GetByCustomerIdAsync(
        Guid customerId,
        CancellationToken cancellationToken);

    Task<Wishlist?> GetByIdAsync(
        Guid wishlistId,
        CancellationToken cancellationToken);

    Task AddAsync(
        Wishlist wishlist,
        CancellationToken cancellationToken);

    void Remove(Wishlist wishlist);

    Task<bool> ExistsAsync(
        Guid customerId,
        CancellationToken cancellationToken);


    Task<int> GetWishlistCountAsync(
    Guid customerId,
    CancellationToken cancellationToken);

    Task<bool> IsProductInWishlistAsync(
        Guid customerId,
        Guid productId,
        CancellationToken cancellationToken);
}