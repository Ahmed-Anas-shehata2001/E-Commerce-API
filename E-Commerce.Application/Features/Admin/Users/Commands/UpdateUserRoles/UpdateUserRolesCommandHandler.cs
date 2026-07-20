using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Admin.Users.Commands.UpdateUserRoles;

public class UpdateUserRolesCommandHandler
    : IRequestHandler<UpdateUserRolesCommand, Result>
{
    private readonly IUserService _userService;

    public UpdateUserRolesCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result> Handle(
        UpdateUserRolesCommand request,
        CancellationToken cancellationToken)
    {
        return await _userService.UpdateUserRolesAsync(
            new UpdateUserRolesRequest
            {
                UserId = request.UserId,
                RoleNames = request.RoleNames
            },
            cancellationToken);
    }
}