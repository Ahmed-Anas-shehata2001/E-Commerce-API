using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Admin.Users.Queries.GetUsers;

public sealed class GetUsersQueryHandler
    : IRequestHandler<GetUsersQuery, Result<PagedResult<UserInfo>>>
{
    private readonly IUserService _userService;

    public GetUsersQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<PagedResult<UserInfo>>> Handle(
      GetUsersQuery request,
      CancellationToken cancellationToken)
    {
        var paginationRequest = new PaginationRequest
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            SearchTerm = request.SearchTerm
        };

        return await _userService.GetUsersAsync(
            paginationRequest,
            cancellationToken);
    }
}