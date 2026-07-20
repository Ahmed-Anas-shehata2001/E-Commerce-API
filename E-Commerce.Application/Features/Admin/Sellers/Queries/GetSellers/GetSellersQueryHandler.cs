using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellers;
using E_Commerce.Domain.Common.Result;
using MediatR;

public sealed class GetSellersQueryHandler
    : IRequestHandler<GetSellersQuery, Result<PagedResult<SellerDto>>>
{
    private readonly IUserService _userService;

    public GetSellersQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<PagedResult<SellerDto>>> Handle(
        GetSellersQuery request,
        CancellationToken cancellationToken)
    {
        var sellers = await _userService.GetSellersAsync(
            request.PageNumber,
            request.PageSize,
            cancellationToken);

        return Result<PagedResult<SellerDto>>.Success(sellers);
    }
}