using E_Commerce.Domain.Common.Result;
using MediatR;

namespace E_Commerce.Application.Features.Identity.Roles
{
    /// <summary>
    /// Query to get role permissions.
    /// </summary>
    public record GetRolePermissionsQuery(Guid RoleId)
      : IRequest<Result<IReadOnlyList<string>>>;
}
