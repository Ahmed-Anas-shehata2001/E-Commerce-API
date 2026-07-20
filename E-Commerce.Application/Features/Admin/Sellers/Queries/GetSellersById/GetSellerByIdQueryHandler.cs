using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellers;
using E_Commerce.Domain.Common.Result;
using MediatR;

public sealed class GetSellerByIdHandler
    : IRequestHandler<GetSellerByIdQuery, Result<SellerDto>>
{
    private readonly IUserService _userService;

    public GetSellerByIdHandler(IUserService userService)
    {
        _userService = userService;
    }

    public Task<Result<SellerDto>> Handle(
        GetSellerByIdQuery request,
        CancellationToken cancellationToken)
    {
        return _userService.GetSellerByIdAsync(
            request.SellerId,
            cancellationToken);
    }
}