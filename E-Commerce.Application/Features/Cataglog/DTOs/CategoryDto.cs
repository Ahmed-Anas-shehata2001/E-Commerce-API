namespace E_Commerce.Application.Features.Catalog.DTOs;

public sealed record CategoryDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;

    public string? Description { get; init; }
}