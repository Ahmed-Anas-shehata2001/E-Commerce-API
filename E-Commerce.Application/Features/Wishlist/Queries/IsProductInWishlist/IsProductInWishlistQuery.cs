using MediatR;

namespace E_Commerce.Application.Features.Customer.Wishlist.Queries.IsProductInWishlist;

public sealed record IsProductInWishlistQuery(Guid ProductId)
    : IRequest<bool>;