using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Features.Identity.Roles.UpdateRolePermissions
{
    public class UpdateRolePermissionsCommandHandler
     : IRequestHandler<UpdateRolePermissionsCommand, Result>
    {
        private readonly IRoleService _roleService;

        public UpdateRolePermissionsCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Result> Handle(
            UpdateRolePermissionsCommand request,
            CancellationToken cancellationToken)
        {
            var updateRequest = new UpdateRolePermissionsRequest
            {
                RoleId = request.RoleId,
                PermissionNames = request.PermissionNames
            };

            return await _roleService.UpdateRolePermissionsAsync(
                updateRequest,
                cancellationToken);
        }
    }
}
