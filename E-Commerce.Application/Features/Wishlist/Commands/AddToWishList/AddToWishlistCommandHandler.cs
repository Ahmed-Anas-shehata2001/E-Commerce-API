using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.Catalog.ProductFeature.Interfaces;
using E_Commerce.Domain.Features.WishlistFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Wishlist.Commands.AddToWishlist;

public sealed class AddToWishlistCommandHandler
    : IRequestHandler<AddToWishlistCommand, Result>
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public AddToWishlistCommandHandler(
        IWishlistRepository wishlistRepository,
        IProductRepository productRepository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        _wishlistRepository = wishlistRepository;
        _productRepository = productRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        AddToWishlistCommand request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.UserId.HasValue)
            return Result.Failure("User is not authenticated.");

        var product = await _productRepository.GetProductByIdAsync(
            request.ProductId,
            cancellationToken);

        if (product is null)
            return Result.Failure("Product not found.");

        var wishlist = await _wishlistRepository.GetByCustomerIdAsync(
            _currentUser.UserId.Value,
            cancellationToken);

        if (wishlist is null)
        {
            wishlist = new Domain.Features.WishlistFeature.Entities.Wishlist(
                _currentUser.UserId.Value);

            await _wishlistRepository.AddAsync(
                wishlist,
                cancellationToken);
        }

        wishlist.AddProduct(request.ProductId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}