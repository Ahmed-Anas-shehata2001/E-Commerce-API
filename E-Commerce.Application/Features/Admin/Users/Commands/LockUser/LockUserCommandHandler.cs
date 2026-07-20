using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Admin.Users.Commands.LockUser;

public class LockUserCommandHandler
    : IRequestHandler<LockUserCommand, Result>
{
    private readonly IUserService _userService;

    public LockUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result> Handle(
        LockUserCommand request,
        CancellationToken cancellationToken)
    {
        return await _userService.LockUserAsync(
            new LockUserRequest
            {
                UserId = request.UserId,
                LockoutEndUtc = request.LockoutEndUtc
            },
            cancellationToken);
    }
}