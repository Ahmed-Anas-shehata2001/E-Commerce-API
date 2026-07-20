using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Admin.Users.Queries.IsUserInRole;

public class IsUserInRoleQueryHandler
    : IRequestHandler<IsUserInRoleQuery, Result<bool>>
{
    private readonly IUserService _userService;

    public IsUserInRoleQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result<bool>> Handle(
        IsUserInRoleQuery request,
        CancellationToken cancellationToken)
    {
        return await _userService.IsUserInRoleAsync(
            request.UserId,
            request.RoleName,
            cancellationToken);
    }
}