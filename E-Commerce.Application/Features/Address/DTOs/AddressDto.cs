namespace E_Commerce.Application.Features.Address.DTOs;

public sealed class AddressDto
{
    public Guid Id { get; init; }

    public string FullName { get; init; } = default!;

    public string PhoneNumber { get; init; } = default!;

    public string Country { get; init; } = default!;

    public string City { get; init; } = default!;

    public string State { get; init; } = default!;

    public string Street { get; init; } = default!;

    public string Building { get; init; } = default!;

    public string? Apartment { get; init; }

    public string PostalCode { get; init; } = default!;

    public bool IsDefault { get; init; }

    public DateTime CreatedAtUtc { get; init; }
}