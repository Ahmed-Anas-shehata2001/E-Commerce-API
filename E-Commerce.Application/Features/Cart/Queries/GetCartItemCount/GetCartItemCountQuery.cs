using MediatR;

namespace E_Commerce.Application.Features.Cart.Queries.GetCartItemCount;

public sealed record GetCartItemCountQuery
    : IRequest<int>;