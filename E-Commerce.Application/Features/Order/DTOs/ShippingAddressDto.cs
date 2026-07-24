

namespace E_Commerce.Application.Features.Order.DTOs
{
    public sealed class ShippingAddressDto
    {
        public string RecipientName { get; init; } = default!;

        public string PhoneNumber { get; init; } = default!;

        public string Country { get; init; } = default!;

        public string City { get; init; } = default!;

        public string State { get; init; } = default!;

        public string Street { get; init; } = default!;

        public string Building { get; init; } = default!;

        public string? Apartment { get; init; }

        public string PostalCode { get; init; } = default!;
    }
}
