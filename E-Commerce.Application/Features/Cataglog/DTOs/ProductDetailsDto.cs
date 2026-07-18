namespace E_Commerce.Application.Features.Catalog.DTOs;

public sealed record ProductDetailsDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;

    public string? Description { get; init; }

    public string SKU { get; init; } = default!;

    public decimal Price { get; init; }

    public int Stock { get; init; }

    public decimal? Weight { get; init; }

    public Guid CategoryId { get; init; }

    public string CategoryName { get; init; } = default!;

    public Guid BrandId { get; init; }

    public string BrandName { get; init; } = default!;

    public Guid SellerId { get; init; }

    public string Status { get; init; } = default!;

    public DateTime CreatedAtUtc { get; init; }

    public DateTime? UpdatedAtUtc { get; init; }
}