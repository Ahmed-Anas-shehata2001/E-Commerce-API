using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Features.WishlistFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Customer.Wishlist.Queries.IsProductInWishlist;

public sealed class IsProductInWishlistHandler
    : IRequestHandler<IsProductInWishlistQuery, bool>
{
    private readonly IWishlistRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public IsProductInWishlistHandler(
        IWishlistRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(
        IsProductInWishlistQuery request,
        CancellationToken ct)
    {
        var wishlist = await _repository.GetByCustomerIdAsync(
            _currentUser.UserId!.Value,
            ct);

        if (wishlist is null)
            return false;

        return wishlist.Items.Any(x => x.ProductId == request.ProductId);
    }
}