namespace E_Commerce.Application.Features.Catalog.DTOs;

public sealed record ReviewDto
{
    public Guid Id { get; init; }

    public Guid CustomerId { get; init; }

    public int Rating { get; init; }

    public string? Comment { get; init; }

    public DateTime CreatedAtUtc { get; init; }
}