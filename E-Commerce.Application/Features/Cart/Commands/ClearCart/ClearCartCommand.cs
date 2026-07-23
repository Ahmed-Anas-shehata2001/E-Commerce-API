using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Commands.ClearCart;

public sealed record ClearCartCommand
    : IRequest<Result>;