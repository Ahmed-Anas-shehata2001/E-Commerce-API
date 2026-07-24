using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Address.Commands.UpdateAddress;

public sealed record UpdateAddressCommand(
    Guid AddressId,
    string FullName,
    string PhoneNumber,
    string Country,
    string City,
    string State,
    string Street,
    string Building,
    string? Apartment,
    string PostalCode)
    : IRequest<Result>;