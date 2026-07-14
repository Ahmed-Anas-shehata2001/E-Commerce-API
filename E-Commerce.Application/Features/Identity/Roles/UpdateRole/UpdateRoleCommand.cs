using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Roles
{
    /// <summary>
    /// Command to update a role.
    /// </summary>
    public record UpdateRoleCommand(
   Guid RoleId,
        string Name,
  string? Description = null
    ) : IRequest<Result>;
}
