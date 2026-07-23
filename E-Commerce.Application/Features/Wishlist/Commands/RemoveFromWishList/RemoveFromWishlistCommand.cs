using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Wishlist.Commands.RemoveFromWishlist;

public sealed record RemoveFromWishlistCommand(
    Guid ProductId)
    : IRequest<Result>;