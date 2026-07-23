using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Interfaces;
using E_Commerce.Domain.Common.Result;
using E_Commerce.Domain.Features.WishlistFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Wishlist.Commands.RemoveFromWishlist;

public sealed class RemoveFromWishlistCommandHandler
    : IRequestHandler<RemoveFromWishlistCommand, Result>
{
    private readonly IWishlistRepository _wishlistRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveFromWishlistCommandHandler(
        IWishlistRepository wishlistRepository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        _wishlistRepository = wishlistRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        RemoveFromWishlistCommand request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.UserId.HasValue)
            return Result.Failure("User is not authenticated.");

        var wishlist = await _wishlistRepository.GetByCustomerIdAsync(
            _currentUser.UserId.Value,
            cancellationToken);

        if (wishlist is null)
            return Result.Success();


        wishlist.RemoveProduct(request.ProductId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}