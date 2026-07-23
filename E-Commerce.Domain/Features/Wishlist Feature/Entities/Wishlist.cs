
using E_Commerce.Domain.Common.Base;

namespace E_Commerce.Domain.Features.WishlistFeature.Entities;

public sealed class Wishlist : AuditableEntity
{
    public Guid CustomerId { get; private set; }


    private readonly List<WishlistItem> _items = [];
    public IReadOnlyCollection<WishlistItem> Items => _items;

    public int Count => _items.Count;
    public bool IsEmpty => !_items.Any();

    private Wishlist() { }

    public Wishlist(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("CustomerId is required.");

        CustomerId = customerId;
    }

    public void AddProduct(Guid productId)
    {
        if (_items.Any(i => i.ProductId == productId))
            return;

        var item = new WishlistItem(Id, productId);
        _items.Add(item);
    }

    public void RemoveProduct(Guid productId)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);

        if (item is null)
            return;

        _items.Remove(item);
    }

    public bool ContainsProduct(Guid productId)
        => _items.Any(i => i.ProductId == productId);

    public void Clear()
    {
        _items.Clear();
    }
}