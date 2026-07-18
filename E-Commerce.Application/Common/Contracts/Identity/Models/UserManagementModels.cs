namespace E_Commerce.Application.Common.Contracts.Identity.Models
{

    public class UserInfo
    {
        // This represents a  ** user profile **.
        public Guid Id { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string? UserName { get; init; }
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string? PhoneNumber { get; init; }
        public bool EmailConfirmed { get; init; }
        public bool TwoFactorEnabled { get; init; }
        public bool IsLockedOut { get; init; }
        public IReadOnlyList<string> Roles { get; init; } = Array.Empty<string>();
        public IReadOnlyList<string> Permissions { get; init; } = Array.Empty<string>();
    }
    public sealed class PagedResult<T>
    {
        // Generic wrapper for paginated list responses (users, roles, permissions, etc.)

        public IReadOnlyList<T> Items { get; init; } = new List<T>();
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; init; }
        public int TotalPages => PageSize == 0 ? 0 : (int)System.Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }

public class PaginationRequest
{
    private const int MaxPageSize = 100;
    private int _pageSize = 20;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = Math.Clamp(value, 1, MaxPageSize);
    }

    public string? SearchTerm { get; set; }
}
    /// <summary>
    /// Request model for updating user information.
    /// </summary>
    public class UpdateUserRequest
    {
        public Guid UserId { get; init; } = default!;
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string? PhoneNumber { get; init; }
    }

    public class UpdateUserRolesRequest
    {
        public Guid UserId { get; init; } = default!;

        public IReadOnlyList<string> RoleNames { get; init; }
            = Array.Empty<string>();
    }

    /// <summary>
    /// Request model for locking a user account.
    /// </summary>
    public class LockUserRequest
    {
        public Guid UserId { get; init; } = default!;
        public DateTimeOffset? LockoutEndUtc { get; init; }
    }

    public sealed class UserRoleRequest
    {
        public Guid UserId { get; init; } = default!;

        public string RoleName { get; init; } = default!;
    }


}
