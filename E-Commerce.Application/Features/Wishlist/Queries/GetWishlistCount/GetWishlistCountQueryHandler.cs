using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Features.WishlistFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Customer.Wishlist.Queries.GetWishlistCount;

public sealed class GetWishlistCountHandler
    : IRequestHandler<GetWishlistCountQuery, int>
{
    private readonly IWishlistRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public GetWishlistCountHandler(
        IWishlistRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(
        GetWishlistCountQuery request,
        CancellationToken ct)
    {
        var wishlist = await _repository.GetByCustomerIdAsync(
            _currentUser.UserId!.Value,
            ct);

        return wishlist?.Items.Count ?? 0;
    }
}