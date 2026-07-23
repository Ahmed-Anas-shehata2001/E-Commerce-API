using MediatR;

namespace E_Commerce.Application.Features.Cart.Queries.GetCartTotal;

public sealed record GetCartTotalQuery
    : IRequest<decimal>;