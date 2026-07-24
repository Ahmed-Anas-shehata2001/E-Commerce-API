using E_Commerce.Application.Features.Address.DTOs;
using E_Commerce.Domain.Common.Result;
using MediatR;


namespace E_Commerce.Application.Features.Address.Quereis.GetAddressById
{
    public sealed record GetDefaultAddressQuery()
     : IRequest<Result<AddressDto>>;
}
