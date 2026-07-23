using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Cart_Feature;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Commands.AddToCart;

public sealed class AddToCartCommandHandler
    : IRequestHandler<AddToCartCommand, Result>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public AddToCartCommandHandler(
        ICartRepository cartRepository,
        IProductRepository productRepository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        AddToCartCommand request,
        CancellationToken ct)
    {
        if (!_currentUser.UserId.HasValue)
            return Result.Failure("User is not authenticated.");

        var product = await _productRepository.GetProductByIdAsync(
            request.ProductId,
            ct);

        if (product is null)
            return Result.Failure("Product not found.");

        var cart = await _cartRepository.GetByCustomerIdAsync(
            _currentUser.UserId.Value,
            ct);

        if (cart is null)
        {
            cart = new Domain.Features.CartFeature.Entities.Cart(_currentUser.UserId.Value);

            await _cartRepository.AddAsync(cart, ct);
        }

        cart.AddProduct(
            request.ProductId,
            request.Quantity);

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}