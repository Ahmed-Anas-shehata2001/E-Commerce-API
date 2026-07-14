using MediatR;
using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Domain.Common.Result;

namespace E_Commerce.Application.Features.Identity.Roles
{
    /// <summary>
    /// Handles getting role permissions.
    /// </summary>
    public class GetRolePermissionsQueryHandler
      : IRequestHandler<GetRolePermissionsQuery, Result<IReadOnlyList<string>>>
    {
        private readonly IRoleService _roleService;

        public GetRolePermissionsQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Result<IReadOnlyList<string>>> Handle(
            GetRolePermissionsQuery request,
            CancellationToken cancellationToken)
        {
            return await _roleService.GetRolePermissionsAsync(
                request.RoleId,
                cancellationToken);
        }
    }

}
