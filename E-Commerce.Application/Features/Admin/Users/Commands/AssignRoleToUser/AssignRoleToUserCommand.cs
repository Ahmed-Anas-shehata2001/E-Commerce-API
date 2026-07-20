using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Admin.Users.Commands.AssignRoleToUser;

public record AssignRoleToUserCommand(
    Guid UserId,
    string RoleName
) : IRequest<Result>;