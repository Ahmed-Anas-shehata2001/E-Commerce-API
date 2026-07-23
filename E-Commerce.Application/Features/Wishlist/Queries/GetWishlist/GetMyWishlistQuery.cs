using E_Commerce.Application.Features.Wishlist.DTOs;
using MediatR;

namespace E_Commerce.Application.Features.Customer.Wishlist.Queries.GetMyWishlist;

public sealed record GetMyWishlistQuery()
    : IRequest<List<WishlistItemDto>>;