using E_Commerce.Application.Features.Address.DTOs;
using MediatR;

namespace E_Commerce.Application.Features.Address.Queries.GetMyAddresses;

public sealed record GetMyAddressesQuery()
    : IRequest<List<AddressDto>>;