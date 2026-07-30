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
