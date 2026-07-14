using E_Commerce.Application.Common.Contracts.Identity;
using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Errors;
using E_Commerce.Domain.Common.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace E_Commerce.Infrastructure.Identity.Services
{
    public class RoleService : IRoleService
    {

        private readonly RoleManager<ApplicationRole> _roleManager;
        private const string PermissionClaimType = "permission";

        public RoleService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Result<IReadOnlyList<RoleDto>>> GetAllRolesAsync(CancellationToken ct = default)
        {
            var roles = await _roleManager.Roles.ToListAsync(ct);
            var dtos = new List<RoleDto>();

            foreach (var role in roles)
            {
                var claims = await _roleManager.GetClaimsAsync(role);
                dtos.Add(MapToDto(role, claims));
            }

            return Result<IReadOnlyList<RoleDto>>.Success(dtos);
        }


        public async Task<Result<RoleDto>> GetRoleByIdAsync( Guid roleId,CancellationToken ct = default)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());

            if (role is null)
                return Result<RoleDto>.Failure(RoleErrors.NotFound(roleId));

            var claims = await _roleManager.GetClaimsAsync(role);

            return Result<RoleDto>.Success(MapToDto(role, claims));
        }

        public async Task<Result<RoleDto>> CreateRoleAsync(CreateRoleRequest request, CancellationToken ct = default)
        {
            var roleName = request.Name.Trim();
            var exists = await _roleManager.RoleExistsAsync(roleName);
            if (exists)
                return Result<RoleDto>.Failure(RoleErrors.AlreadyExists(roleName));

            var role = new ApplicationRole
            {
                Name = roleName,
                Description = request.Description
            };
            // If you extend IdentityRole with a Description property, set it here.

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
                return Result<RoleDto>.Failure(
             RoleErrors.CreateFailed(GetIdentityErrors(result)));

            return Result<RoleDto>.Success(
       MapToDto(role, new List<Claim>())
   );
        }

        public async Task<Result> UpdateRoleAsync(UpdateRoleRequest request, CancellationToken ct = default)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role is null)
                return Result.Failure(RoleErrors.NotFound(request.RoleId));

            role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(role);

            return result.Succeeded
      ? Result.Success()
      : Result.Failure(
          RoleErrors.UpdateFailed(GetIdentityErrors(result)));
        }

        public async Task<Result> DeleteRoleAsync(Guid roleId, CancellationToken ct = default)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role is null)
                return Result.Failure(RoleErrors.NotFound(roleId));

            var result = await _roleManager.DeleteAsync(role);

            return result.Succeeded
     ? Result.Success()
     : Result.Failure(
         RoleErrors.DeleteFailed(GetIdentityErrors(result)));
        }

        public async Task<Result<IReadOnlyList<string>>> GetRolePermissionsAsync(Guid roleId, CancellationToken ct = default)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role is null)
                return Result<IReadOnlyList<string>>.Failure(RoleErrors.NotFound(roleId));

            var claims = await _roleManager.GetClaimsAsync(role);
            var permissions = claims
                .Where(c => c.Type == PermissionClaimType)
                .Select(c => c.Value)
                .ToList();

            return Result<IReadOnlyList<string>>.Success(permissions);
        }

        public async Task<Result> UpdateRolePermissionsAsync(UpdateRolePermissionsRequest request, CancellationToken ct = default)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            if (role is null)
                return Result.Failure(RoleErrors.NotFound(request.RoleId));

            var existingClaims = (await _roleManager.GetClaimsAsync(role))
                .Where(c => c.Type == PermissionClaimType)
                .ToList();

            // Remove all existing permission claims, then re-add the requested set.
            foreach (var claim in existingClaims)
            {
                var removeResult = await _roleManager.RemoveClaimAsync(role, claim);
                if (!removeResult.Succeeded)
                    return Result.Failure(
    RoleErrors.UpdateFailed(GetIdentityErrors(removeResult)));
            }

            foreach (var permission in request.PermissionNames.Distinct())
            {
                var addResult = await _roleManager.AddClaimAsync(role, new Claim(PermissionClaimType, permission));
                if (!addResult.Succeeded)
                    return Result.Failure(
     RoleErrors.UpdateFailed(GetIdentityErrors(addResult)));
            }

            return Result.Success();
        }

        // ---------- helpers ----------
        private static RoleDto MapToDto(ApplicationRole role, IList<Claim> claims) => new()
        {
            Id = role.Id,
            Name = role.Name!,
            Description = role.Description,
            Permissions = claims
          .Where(c => c.Type == PermissionClaimType)
          .Select(c => c.Value)
          .ToList()
        };


        // identity errors
        private static string GetIdentityErrors(IdentityResult result)
        {
            return string.Join(", ", result.Errors.Select(e => e.Description));
        }


    }
}
