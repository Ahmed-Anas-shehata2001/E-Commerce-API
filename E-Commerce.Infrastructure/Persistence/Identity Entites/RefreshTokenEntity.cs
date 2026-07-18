

using E_Commerce.Infrastructure.Identity.Services;

namespace E_Commerce.Infrastructure.Identity.Identity_Entites
{
    public enum RefreshTokenRevocationReason
    {
        Logout,
        Rotated,
        PasswordChanged,
        PasswordReset,
        UserDisabled,
        RefreshTokenReuseDetected,
        AdminRevoked
    }
    public class RefreshTokenEntity
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = default!;

        public string TokenHash { get; set; } = string.Empty;

        public Guid SessionId { get; set; }

        public UserSession Session { get; set; } = default!;

        public DateTime CreatedAtUtc { get; set; }

        public DateTime ExpiresAtUtc { get; set; }

        public DateTime? RevokedAtUtc { get; set; }
        public RefreshTokenRevocationReason? RevocationReason { get; set; }

        public string? CreatedByIp { get; set; }

        public string? RevokedByIp { get; set; }

        public bool IsRevoked => RevokedAtUtc != null;
        public bool IsExpired => DateTime.UtcNow >= ExpiresAtUtc;
        public bool IsActive => !IsRevoked && !IsExpired;

        public Guid? ReplacedByTokenId { get; set; }
        public RefreshTokenEntity? ReplacedByToken { get; set; }

        public DateTime? LastUsedAtUtc { get; set; }
    }
}
