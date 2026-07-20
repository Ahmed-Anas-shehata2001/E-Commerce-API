using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Admin.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId)
    : IRequest<Result>;