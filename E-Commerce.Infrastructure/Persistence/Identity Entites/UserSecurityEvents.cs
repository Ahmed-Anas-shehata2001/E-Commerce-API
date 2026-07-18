namespace E_Commerce.Infrastructure.Identity.Identity_Entites.UserSecurityEvents
{
    //Yes. That's actually a very common design for an audit/security log table. Its purpose is to record security-related actions that occur on a user's account.
    public enum UserSecurityEventType
    {

        LoginSucceeded,
        LoginFailed,
        Logout,

        RefreshTokenIssued,
        RefreshTokenRotated,
        RefreshTokenRevoked,
        RefreshTokenReuseDetected,

        PasswordChanged,
        PasswordResetRequested,
        PasswordReset,

        EmailConfirmed,
        EmailChanged,

        PhoneChanged,
        PhoneConfirmed,

        TwoFactorEnabled,
        TwoFactorDisabled,
        TwoFactorSucceeded,
        TwoFactorFailed,

        ExternalLoginSucceeded,
        ExternalLoginFailed,

        AccountLocked,
        AccountUnLocked,
        UserDisabled,
        UserEnabled,

        UserDeleted
    }
    public class UserSecurityEvent
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = default!;

        public UserSecurityEventType EventType { get; set; }

        public string? Details { get; set; }

        public string? IpAddress { get; set; }

        public DateTime OccurredAtUtc { get; set; } = DateTime.UtcNow;
    }
}
