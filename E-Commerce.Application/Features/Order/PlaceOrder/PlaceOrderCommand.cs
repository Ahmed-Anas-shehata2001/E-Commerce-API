using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Order.Commands.PlaceOrder;

public sealed record PlaceOrderCommand(
    Guid AddressId)
    : IRequest<Result<Guid>>;