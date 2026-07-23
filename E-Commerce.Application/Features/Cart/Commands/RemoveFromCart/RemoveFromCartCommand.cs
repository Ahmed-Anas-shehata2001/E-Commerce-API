using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Commands.RemoveFromCart;

public sealed record RemoveFromCartCommand(
    Guid ProductId)
    : IRequest<Result>;