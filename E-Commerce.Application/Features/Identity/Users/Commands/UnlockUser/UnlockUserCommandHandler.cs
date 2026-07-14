using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Identity.Users;

public class UnlockUserCommandHandler
    : IRequestHandler<UnlockUserCommand, Result>
{
    private readonly IUserService _userService;

    public UnlockUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result> Handle(
        UnlockUserCommand request,
        CancellationToken cancellationToken)
    {
        return await _userService.UnlockUserAsync(
            request.UserId,
            cancellationToken);
    }
}