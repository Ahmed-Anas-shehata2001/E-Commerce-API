using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Features.Wishlist.DTOs;
using E_Commerce.Domain.Features.WishlistFeature.Interfaces;
using MediatR;

namespace E_Commerce.Application.Features.Customer.Wishlist.Queries.GetMyWishlist;

public sealed class GetMyWishlistHandler
    : IRequestHandler<GetMyWishlistQuery, List<WishlistItemDto>>
{
    private readonly IWishlistRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public GetMyWishlistHandler(
        IWishlistRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<List<WishlistItemDto>> Handle(
        GetMyWishlistQuery request,
        CancellationToken ct)
    {
        var wishlist = await _repository.GetByCustomerIdAsync(
            _currentUser.UserId!.Value,
            ct);

        if (wishlist is null)
            return [];

        return wishlist.Items
            .Select(x => new WishlistItemDto
            {
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                Price = x.Product.CurrentPrice,
                ImageUrl = null,
                InStock = x.Product.Stock > 0
            })
            .ToList();
    }
}