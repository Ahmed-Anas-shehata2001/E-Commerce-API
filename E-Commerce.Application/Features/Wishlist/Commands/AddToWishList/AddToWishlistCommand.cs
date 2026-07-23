using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Wishlist.Commands.AddToWishlist;

public sealed record AddToWishlistCommand(
    Guid ProductId)
    : IRequest<Result>;