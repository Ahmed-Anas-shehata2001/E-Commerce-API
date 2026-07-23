using E_Commerce.Domain.Common.Base;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Entities;

namespace E_Commerce.Domain.Features.CartFeature.Entities;

public sealed class CartItem : AuditableEntity
{
    public Guid CartId { get; private set; }
    public Cart Cart { get; private set; } = default!;

    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = default!;

    public int Quantity { get; private set; }

    public decimal TotalPrice
    => Product.CurrentPrice * Quantity;

    private CartItem() { }

    internal CartItem(
        Guid cartId,
        Guid productId,
        int quantity)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("ProductId is required.");

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
    }

    internal void IncreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        Quantity += quantity;
    }

    internal void DecreaseQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        if (Quantity - quantity <= 0)
            throw new InvalidOperationException("Quantity cannot be less than one.");

        Quantity -= quantity;
    }

    internal void UpdateQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        Quantity = quantity;
    }




}