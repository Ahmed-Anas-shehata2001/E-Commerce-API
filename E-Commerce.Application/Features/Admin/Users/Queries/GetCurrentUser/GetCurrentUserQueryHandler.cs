using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Admin.Users.Queries.GetCurrentUser;

public sealed class GetCurrentUserQueryHandler
    : IRequestHandler<GetCurrentUserQuery, Result<UserInfo>>
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUser;

    public GetCurrentUserQueryHandler(
        IUserService userService,
        ICurrentUserService currentUser)
    {
        _userService = userService;
        _currentUser = currentUser;
    }

    public async Task<Result<UserInfo>> Handle(
        GetCurrentUserQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated || _currentUser.UserId is null)
        {
            return Result<UserInfo>.Failure(
                "User is not authenticated.");
        }

        return await _userService.GetUserByIdAsync(
            _currentUser.UserId.Value,
            cancellationToken);
    }
}