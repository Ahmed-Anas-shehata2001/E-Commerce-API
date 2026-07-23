
using E_Commerce.Domain.Common.Base;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Entities;

namespace E_Commerce.Domain.Features.WishlistFeature.Entities;

public sealed class WishlistItem : AuditableEntity
{
    public Guid WishlistId { get; private set; }
    public Wishlist Wishlist { get; private set; } = default!;

    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = default!;


    private WishlistItem() { }

 

    internal WishlistItem(Guid wishlistId, Guid productId)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("ProductId is required.");
        WishlistId = wishlistId;
        ProductId = productId;
    }
}