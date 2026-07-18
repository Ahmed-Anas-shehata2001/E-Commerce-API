namespace E_Commerce.Application.Features.Catalog.DTOs;

public sealed record ProductDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;

    public string SKU { get; init; } = default!;

    public decimal Price { get; init; }

    public int Stock { get; init; }

    public string CategoryName { get; init; } = default!;

    public string BrandName { get; init; } = default!;

    public string Status { get; init; } = default!;
}