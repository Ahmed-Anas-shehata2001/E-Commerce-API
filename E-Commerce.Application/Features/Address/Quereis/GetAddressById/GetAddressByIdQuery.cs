using E_Commerce.Application.Features.Address.DTOs;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Address.Queries.GetAddressById;

public sealed record GetAddressByIdQuery(
    Guid AddressId)
    : IRequest<Result<AddressDto>>;