using E_Commerce.Domain.Common.Base;
namespace E_Commerce.Domain.Features.OrderFeature.Entities;

public sealed class OrderItem : AuditableEntity
{
    public Guid OrderId { get; private set; }
    public Order Order { get; private set; } = default!;

    public Guid ProductId { get; private set; }

    public string ProductName { get; private set; } = default!;

    public decimal UnitPrice { get; private set; }

    public int Quantity { get; private set; }

    public decimal TotalPrice
        => UnitPrice * Quantity;

    private OrderItem() { }

    internal OrderItem(
        Guid orderId,
        Guid productId,
        string productName,
        decimal unitPrice,
        int quantity)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("ProductId is required.");

        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name is required.");

        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative.");

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        OrderId = orderId;
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
}