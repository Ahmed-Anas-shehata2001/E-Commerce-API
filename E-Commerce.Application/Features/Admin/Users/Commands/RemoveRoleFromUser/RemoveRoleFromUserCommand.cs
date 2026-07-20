using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Admin.Users.Commands.RemoveRoleFromUser;

public record RemoveRoleFromUserCommand(
    Guid UserId,
    string RoleName
) : IRequest<Result>;