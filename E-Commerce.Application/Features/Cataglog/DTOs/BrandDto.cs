namespace E_Commerce.Application.Features.Catalog.DTOs;

public sealed record BrandDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;

    public string? Description { get; init; }

    public DateTime? CreatedAtUTC { get; init; }
    public DateTime? UpdateAtUTC { get; init; }
}