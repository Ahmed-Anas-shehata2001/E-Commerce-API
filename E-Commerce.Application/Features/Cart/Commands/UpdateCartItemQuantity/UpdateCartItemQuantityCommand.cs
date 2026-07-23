using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Commands.UpdateCartItemQuantity;

public sealed record UpdateCartItemQuantityCommand(
    Guid ProductId,
    int Quantity)
    : IRequest<Result>;