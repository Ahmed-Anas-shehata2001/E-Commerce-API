using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Address.Commands.DeleteAddress;

public sealed record DeleteAddressCommand(
    Guid AddressId)
    : IRequest<Result>;