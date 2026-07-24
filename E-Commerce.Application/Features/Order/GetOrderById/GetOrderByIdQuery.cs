using E_Commerce.Application.Features.Order.DTOs;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Order.Queries.GetOrderById;

public sealed record GetOrderByIdQuery(
    Guid OrderId)
    : IRequest<Result<OrderDto>>;