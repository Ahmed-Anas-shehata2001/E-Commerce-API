using E_Commerce.Application.Common.Contracts.Identity.Models;
using E_Commerce.Domain.Common.Result;


namespace E_Commerce.Application.Common.Contracts.Identity
{
    /// <summary>
    /// Service for managing user information and metadata.
    /// </summary>
    public interface IUserService
    {
        Task<Result<UserInfo>> GetUserByIdAsync(Guid userId, CancellationToken ct = default);
        Task<Result<UserInfo>> GetUserByEmailAsync(string email, CancellationToken ct = default);

        /// <summary>
        /// Retrieves all users with pagination and optional search.
        /// </summary>
        Task<Result<PagedResult<UserInfo>>> GetUsersAsync(PaginationRequest request, CancellationToken ct = default);

        Task<Result> UpdateUserAsync(UpdateUserRequest request, CancellationToken ct = default);
        Task<Result> DeleteUserAsync(Guid userId, CancellationToken ct = default);

        Task<Result> LockUserAsync(LockUserRequest request, CancellationToken ct = default);

        Task<Result> UnlockUserAsync(Guid userId, CancellationToken ct = default);

        Task<Result> AssignRoleToUserAsync(UserRoleRequest request, CancellationToken ct = default);

        Task<Result> RemoveRoleFromUserAsync(UserRoleRequest request, CancellationToken ct = default);

        Task<Result<bool>> IsUserInRoleAsync(Guid userId, string roleName, CancellationToken ct = default);
        Task<Result<IReadOnlyList<string>>> GetUserRolesAsync(Guid userId, CancellationToken ct = default);
        Task<Result> UpdateUserRolesAsync(UpdateUserRolesRequest request, CancellationToken ct = default);
        Task<Result<IReadOnlyList<string>>> GetUserPermissionsAsync(Guid userId, CancellationToken ct = default);
    }
}
