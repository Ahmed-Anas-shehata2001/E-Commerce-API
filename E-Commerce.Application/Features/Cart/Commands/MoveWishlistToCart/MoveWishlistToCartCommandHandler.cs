using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Cart_Feature;
using E_Commerce.Domain.Features.WishlistFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Cart.Commands.MoveWishlistToCart;

public sealed class MoveWishlistToCartCommandHandler
    : IRequestHandler<MoveWishlistToCartCommand, Result>
{
    private readonly ICartRepository _cartRepository;
    private readonly IWishlistRepository _wishlistRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public MoveWishlistToCartCommandHandler(
        ICartRepository cartRepository,
        IWishlistRepository wishlistRepository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _wishlistRepository = wishlistRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        MoveWishlistToCartCommand request,
        CancellationToken ct)
    {
        if (!_currentUser.UserId.HasValue)
            return Result.Failure("User is not authenticated.");

        var customerId = _currentUser.UserId.Value;

        var wishlist = await _wishlistRepository.GetByCustomerIdAsync(
            customerId,
            ct);

        if (wishlist is null || !wishlist.Items.Any())
            return Result.Success();

        var cart = await _cartRepository.GetByCustomerIdAsync(
            customerId,
            ct);

        if (cart is null)
        {
            cart = new Domain.Features.CartFeature.Entities.Cart(customerId);

            await _cartRepository.AddAsync(cart, ct);
        }

        foreach (var item in wishlist.Items.ToList())
        {
            var product = item.Product;

            // Product no longer exists
            if (product is null)
                continue;

            // Ignore deleted products
            if (product.IsDeleted)
                continue;



            // Ignore unavailable products
            if (product.Stock <= 0)
                continue;

            // Add to cart (or increase quantity if already exists)
            cart.AddProduct(product.Id, 1);
        }

        // Empty the wishlist after moving
        wishlist.Clear();

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}