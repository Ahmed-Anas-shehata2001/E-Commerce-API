using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Features.Cart.DTOs;
using E_Commerce.Domain.Features.Cart_Feature;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Queries.GetMyCart;

public sealed class GetMyCartQueryHandler
    : IRequestHandler<GetMyCartQuery, CartDto>
{
    private readonly ICartRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public GetMyCartQueryHandler(
        ICartRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<CartDto> Handle(
        GetMyCartQuery request,
        CancellationToken ct)
    {
        var cart = await _repository.GetByCustomerIdAsync(
            _currentUser.UserId!.Value,
            ct);

        if (cart is null)
        {
            return new CartDto
            {
                CustomerId = _currentUser.UserId.Value
            };
        }

        return new CartDto
        {
            CartId = cart.Id,
            CustomerId = cart.CustomerId,

            Items = cart.Items.Select(i => new CartItemDto
            {
                ProductId = i.ProductId,
                ProductName = i.Product.Name,
                UnitPrice = i.Product.CurrentPrice,
                Quantity = i.Quantity,
                TotalPrice = i.Product.CurrentPrice * i.Quantity,
                InStock = i.Product.Stock > 0,
                ImageUrl = null
            }).ToList(),

            Total = cart.Items.Sum(i =>
                i.Product.CurrentPrice * i.Quantity)
        };
    }
}