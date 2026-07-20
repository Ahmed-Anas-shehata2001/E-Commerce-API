using MediatR;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Admin.Roles.DeleteRole
{
    /// <summary>
    /// Command to delete a role.
    /// </summary>
    public record DeleteRoleCommand(
   Guid RoleId
   ) : IRequest<Result>;
}
