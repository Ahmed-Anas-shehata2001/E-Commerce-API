using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Features.Admin.Sellers.Queries.GetSellerStatisitics;
using E_Commerce.Domain.Common.Result;
using MediatR;

public sealed class GetSellerStatisticsHandler
    : IRequestHandler<GetSellerStatisticsQuery,
        Result<SellerStatisticsDto>>
{
    private readonly IUserService _userService;

    public GetSellerStatisticsHandler(
        IUserService userService)
    {
        _userService = userService;
    }

    public Task<Result<SellerStatisticsDto>> Handle(
        GetSellerStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        return _userService.GetSellerStatisticsAsync(
            request.SellerId,
            cancellationToken);
    }
}