using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Commands.AddToCart;

public sealed record AddToCartCommand(
    Guid ProductId,
    int Quantity)
    : IRequest<Result>;