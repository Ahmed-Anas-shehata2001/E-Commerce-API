using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Users;

public record IsUserInRoleQuery(
    Guid UserId,
    string RoleName
) : IRequest<Result<bool>>;