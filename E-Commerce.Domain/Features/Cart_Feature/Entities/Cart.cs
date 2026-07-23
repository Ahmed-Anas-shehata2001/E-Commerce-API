using E_Commerce.Domain.Common.Base;
namespace E_Commerce.Domain.Features.CartFeature.Entities;

public sealed class Cart : AuditableEntity
{
    public Guid CustomerId { get; private set; }

    private readonly List<CartItem> _items = [];
    public IReadOnlyCollection<CartItem> Items => _items;

    private Cart() { }

    public Cart(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("CustomerId is required.");

        CustomerId = customerId;
    }

    public void AddProduct(Guid productId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        var item = _items.FirstOrDefault(x => x.ProductId == productId);

        if (item is null)
        {
            _items.Add(new CartItem(Id, productId, quantity));
            return;
        }

        item.IncreaseQuantity(quantity);
    }

    public void RemoveProduct(Guid productId)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId);

        if (item is null)
            return;

        _items.Remove(item);
    }

    public void UpdateQuantity(Guid productId, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        var item = _items.FirstOrDefault(x => x.ProductId == productId);

        if (item is null)
            throw new InvalidOperationException("Product not found in cart.");

        item.UpdateQuantity(quantity);
    }


    public void Clear()
    {
        _items.Clear();
    }



    // ----------------------------
    // Domain calculations
    // ----------------------------

    public int CalculateItemsCount()
        => _items.Sum(x => x.Quantity);

    public decimal CalculateTotalPrice()
        => _items.Sum(x => x.TotalPrice);
}