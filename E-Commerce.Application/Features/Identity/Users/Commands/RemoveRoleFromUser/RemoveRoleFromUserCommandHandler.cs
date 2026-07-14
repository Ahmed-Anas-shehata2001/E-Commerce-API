using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Users;

public class RemoveRoleFromUserCommandHandler
    : IRequestHandler<RemoveRoleFromUserCommand, Result>
{
    private readonly IUserService _userService;

    public RemoveRoleFromUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result> Handle(
        RemoveRoleFromUserCommand request,
        CancellationToken cancellationToken)
    {
        return await _userService.RemoveRoleFromUserAsync(
            new UserRoleRequest
            {
                UserId = request.UserId,
                RoleName = request.RoleName
            },
            cancellationToken);
    }
}