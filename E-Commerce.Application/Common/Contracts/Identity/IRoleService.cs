using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;


namespace E_Commerce.Application.Common.Contracts.Identity
{
    // Role managment 
    public interface IRoleService
    {

        Task<Result<IReadOnlyList<RoleDto>>> GetAllRolesAsync(CancellationToken ct = default);
        Task<Result<RoleDto>> GetRoleByIdAsync(Guid roleId, CancellationToken ct = default);

        Task<Result<RoleDto>> CreateRoleAsync(CreateRoleRequest request, CancellationToken ct = default);
        Task<Result> UpdateRoleAsync(UpdateRoleRequest request, CancellationToken ct = default);
        Task<Result> DeleteRoleAsync(Guid roleId, CancellationToken ct = default);
        Task<Result<IReadOnlyList<string>>> GetRolePermissionsAsync(Guid roleId, CancellationToken ct = default);

        // remove permission or add permissions
        Task<Result> UpdateRolePermissionsAsync(UpdateRolePermissionsRequest request, CancellationToken ct = default);
  
    }
}
