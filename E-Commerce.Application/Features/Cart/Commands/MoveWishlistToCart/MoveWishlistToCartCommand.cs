using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Commands.MoveWishlistToCart;

public sealed record MoveWishlistToCartCommand
    : IRequest<Result>;