namespace E_Commerce.Application.Features.Cart.DTOs;

    public sealed class CartDto
    {
        public Guid CartId { get; init; }

        public Guid CustomerId { get; init; }

        public decimal Total { get; init; }

        public List<CartItemDto> Items { get; init; } = [];
    }


public sealed class CartItemDto
{
    public Guid ProductId { get; init; }

    public string ProductName { get; init; } = default!;

    public decimal UnitPrice { get; init; }

    public int Quantity { get; init; }

    public decimal TotalPrice { get; init; }

    public bool InStock { get; init; }

    public string? ImageUrl { get; init; }
}