using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Address.Commands.SetDefaultAddress;

public sealed record SetDefaultAddressCommand(
    Guid AddressId)
    : IRequest<Result>;