using E_Commerce.Domain.Common.Base;
using E_Commerce.Domain.Features.AddressFeature.Entities;

namespace E_Commerce.Domain.Features.OrderFeature.Entities;

public enum OrderStatus
{
    Pending = 1,
    Confirmed = 2,
    Processing = 3,
    Shipped = 4,
    Delivered = 5,
    Cancelled = 6
}


public sealed class Order : AuditableEntity
{
    public Guid CustomerId { get; private set; }

    // shipping address
    public Guid ShippingAddressId { get; private set; }
    public Address ShippingAddress { get; private set; } = default!;


    // Shipping Address Snapshot
    public string RecipientName { get; private set; } = default!;

    public string PhoneNumber { get; private set; } = default!;

    public string Country { get; private set; } = default!;

    public string City { get; private set; } = default!;

    public string State { get; private set; } = default!;

    public string Street { get; private set; } = default!;

    public string Building { get; private set; } = default!;

    public string? Apartment { get; private set; }

    public string PostalCode { get; private set; } = default!;

    public OrderStatus Status { get; private set; }

    public DateTime OrderedAtUtc { get; private set; }

    private readonly List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem> Items => _items;

    private Order() { }

    public Order(
        Guid customerId,
        Guid shippingAddressId,
        string recipientName,
        string phoneNumber,
        string country,
        string city,
        string state,
        string street,
        string building,
        string? apartment,
        string postalCode)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("CustomerId is required.");

        if (shippingAddressId == Guid.Empty)
            throw new ArgumentException("ShippingAddressId is required.");

        CustomerId = customerId;
        ShippingAddressId = shippingAddressId;

        RecipientName = recipientName;
        PhoneNumber = phoneNumber;
        Country = country;
        City = city;
        State = state;
        Street = street;
        Building = building;
        Apartment = apartment;
        PostalCode = postalCode;
        Status = OrderStatus.Pending;
        OrderedAtUtc = DateTime.UtcNow;
    }

    public void AddItem(
        Guid productId,
        string productName,
        decimal unitPrice,
        int quantity)
    {
        _items.Add(new OrderItem(
            Id,
            productId,
            productName,
            unitPrice,
            quantity));
    }

    public decimal CalculateTotalPrice()
        => _items.Sum(x => x.TotalPrice);

    public int CalculateTotalItems()
        => _items.Sum(x => x.Quantity);


    public void Cancel()
    {
        if (Status == OrderStatus.Cancelled)
            throw new InvalidOperationException("Order is already cancelled.");

        if (Status == OrderStatus.Shipped)
            throw new InvalidOperationException("Shipped orders cannot be cancelled.");

        if (Status == OrderStatus.Delivered)
            throw new InvalidOperationException("Delivered orders cannot be cancelled.");

        Status = OrderStatus.Cancelled;
    }


    public void Confirm()
    {
        if (Status != OrderStatus.Pending)
            throw new InvalidOperationException("Only pending orders can be confirmed.");

        Status = OrderStatus.Confirmed;
    }

    public void StartProcessing()
    {
        if (Status != OrderStatus.Confirmed)
            throw new InvalidOperationException("Only confirmed orders can be processed.");

        Status = OrderStatus.Processing;
    }


    public void Ship()
    {
        if (Status != OrderStatus.Processing)
            throw new InvalidOperationException("Only processing orders can be shipped.");

        Status = OrderStatus.Shipped;
    }


    public void Deliver()
    {
        if (Status != OrderStatus.Shipped)
            throw new InvalidOperationException("Only shipped orders can be delivered.");

        Status = OrderStatus.Delivered;
    }
}