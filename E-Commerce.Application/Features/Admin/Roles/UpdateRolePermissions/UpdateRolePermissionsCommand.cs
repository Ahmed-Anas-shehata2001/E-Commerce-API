using E_Commerce.Domain.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Features.Admin.Roles.UpdateRolePermissions
{
    public record UpdateRolePermissionsCommand(
      Guid RoleId,
      IReadOnlyList<string> PermissionNames)
      : IRequest<Result>;
}
