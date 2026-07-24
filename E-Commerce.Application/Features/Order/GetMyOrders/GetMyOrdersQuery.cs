using E_Commerce.Application.Features.Order.DTOs;
using MediatR;

namespace E_Commerce.Application.Features.Order.Queries.GetMyOrders;

public sealed record GetMyOrdersQuery()
    : IRequest<List<OrderDto>>;