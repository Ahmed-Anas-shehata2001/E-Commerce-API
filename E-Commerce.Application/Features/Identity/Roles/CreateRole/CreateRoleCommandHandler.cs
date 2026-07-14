using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Roles
{
    /// <summary>
    /// Handles creating a new role.
    /// </summary>
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<RoleDto>>
    {
        private readonly IRoleService _roleService;

        public CreateRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Result<RoleDto>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var createRequest = new CreateRoleRequest
            {
                Name = request.Name,
                Description = request.Description
            };

            return await _roleService.CreateRoleAsync(createRequest, cancellationToken);
        }
    }
}
