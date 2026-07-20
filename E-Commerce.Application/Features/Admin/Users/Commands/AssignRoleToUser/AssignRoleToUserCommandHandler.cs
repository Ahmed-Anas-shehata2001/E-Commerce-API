using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Admin.Users.Commands.AssignRoleToUser;

public class AssignRoleToUserCommandHandler
    : IRequestHandler<AssignRoleToUserCommand, Result>
{
    private readonly IUserService _userService;

    public AssignRoleToUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result> Handle(
        AssignRoleToUserCommand request,
        CancellationToken cancellationToken)
    {
        return await _userService.AssignRoleToUserAsync(
            new UserRoleRequest
            {
                UserId = request.UserId,
                RoleName = request.RoleName
            },
            cancellationToken);
    }
}