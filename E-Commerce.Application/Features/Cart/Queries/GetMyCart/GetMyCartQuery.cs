using E_Commerce.Application.Features.Cart.DTOs;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Queries.GetMyCart;

public sealed record GetMyCartQuery
    : IRequest<CartDto>;