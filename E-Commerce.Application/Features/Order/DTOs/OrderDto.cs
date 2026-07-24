namespace E_Commerce.Application.Features.Order.DTOs;

using E_Commerce.Domain.Features.OrderFeature.Entities;

public sealed class OrderDto
{
    public Guid Id { get; init; }

    public OrderStatus Status { get; init; }

    public DateTime OrderedAtUtc { get; init; }

    public int TotalItems { get; init; }

    public decimal TotalPrice { get; init; }

    // Shipping Address Snapshot
    public string RecipientName { get; init; } = default!;

    public string PhoneNumber { get; init; } = default!;

    public string Country { get; init; } = default!;

    public string City { get; init; } = default!;

    public string State { get; init; } = default!;

    public string Street { get; init; } = default!;

    public string Building { get; init; } = default!;

    public string? Apartment { get; init; }

    public string PostalCode { get; init; } = default!;

    public List<OrderItemDto> Items { get; init; } = [];
    public ShippingAddressDto ShippingAddress { get; init; } = default!;

}