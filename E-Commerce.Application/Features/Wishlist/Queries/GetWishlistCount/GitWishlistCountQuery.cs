using MediatR;

namespace E_Commerce.Application.Features.Customer.Wishlist.Queries.GetWishlistCount;

public sealed record GetWishlistCountQuery()
    : IRequest<int>;