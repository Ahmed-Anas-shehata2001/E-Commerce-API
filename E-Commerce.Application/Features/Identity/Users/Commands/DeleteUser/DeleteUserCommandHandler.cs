using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Identity.Users;

public class DeleteUserCommandHandler
    : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUserService _userService;

    public DeleteUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Result> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken)
    {
        return await _userService.DeleteUserAsync(
            request.UserId,
            cancellationToken);
    }
}