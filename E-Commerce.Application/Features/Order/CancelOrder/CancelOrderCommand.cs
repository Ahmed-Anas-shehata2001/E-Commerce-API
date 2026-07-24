using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Order.Commands.CancelOrder;

public sealed record CancelOrderCommand(
    Guid OrderId)
    : IRequest<Result>;