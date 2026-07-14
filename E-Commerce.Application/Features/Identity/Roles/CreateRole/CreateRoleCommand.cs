using MediatR;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Roles
{
    /// <summary>
    /// Command to create a new role.
    /// </summary>
 public record CreateRoleCommand(
  string Name,
        string? Description = null
    ) : IRequest<Result<RoleDto>>;
}
