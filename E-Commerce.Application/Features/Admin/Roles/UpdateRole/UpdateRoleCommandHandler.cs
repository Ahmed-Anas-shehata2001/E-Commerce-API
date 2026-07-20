using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Admin.Roles.UpdateRole
{
    /// <summary>
    /// Handles updating a role.
    /// </summary>
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result>
    {
        private readonly IRoleService _roleService;

        public UpdateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var updateRequest = new UpdateRoleRequest
            {
                RoleId = request.RoleId,
                Name = request.Name,
                Description = request.Description
            };

            return await _roleService.UpdateRoleAsync(updateRequest, cancellationToken);
        }
    }
}
