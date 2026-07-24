using E_Commerce.Domain.Common.Base;

namespace E_Commerce.Domain.Features.AddressFeature.Entities;

public sealed class Address : AuditableEntity
{
    public Guid CustomerId { get; private set; }

    public string FullName { get; private set; } = default!;

    public string PhoneNumber { get; private set; } = default!;

    public string Country { get; private set; } = default!;

    public string City { get; private set; } = default!;

    public string State { get; private set; } = default!;

    public string Street { get; private set; } = default!;

    public string Building { get; private set; } = default!;

    public string? Apartment { get; private set; }

    public string PostalCode { get; private set; } = default!;

    public bool IsDefault { get; private set; }

    private Address() { }

    public Address(
        Guid customerId,
        string fullName,
        string phoneNumber,
        string country,
        string city,
        string state,
        string street,
        string building,
        string? apartment,
        string postalCode,
        bool isDefault)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("CustomerId is required.");

        CustomerId = customerId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Country = country;
        City = city;
        State = state;
        Street = street;
        Building = building;
        Apartment = apartment;
        PostalCode = postalCode;
        IsDefault = isDefault;
    }

    public void SetDefault()
        => IsDefault = true;

    public void RemoveDefault()
        => IsDefault = false;

    public void Update(
        string fullName,
        string phoneNumber,
        string country,
        string city,
        string state,
        string street,
        string building,
        string? apartment,
        string postalCode)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Country = country;
        City = city;
        State = state;
        Street = street;
        Building = building;
        Apartment = apartment;
        PostalCode = postalCode;
    }

    public void SetDefault(bool isDefault)
    {
        IsDefault = isDefault;
    }
}