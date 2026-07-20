using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Admin.Users.Commands.UpdateUserRoles;

public record UpdateUserRolesCommand(
    Guid UserId,
    IReadOnlyList<string> RoleNames
) : IRequest<Result>;