using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Admin.Users.Queries.IsUserInRole;

public record IsUserInRoleQuery(
    Guid UserId,
    string RoleName
) : IRequest<Result<bool>>;