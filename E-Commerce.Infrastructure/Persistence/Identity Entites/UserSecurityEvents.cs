using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Identity.Identity_Entites.UserSecurityEvents
{

    public enum SecurityEventType
    {
        PasswordChanged = 1,

        EmailChanged,

        EmailConfirmed,

        PhoneChanged,

        PhoneConfirmed,

        LoginSucceeded,

        LoginFailed,

        TwoFactorEnabled,

        TwoFactorDisabled,

        AccountLocked,

        AccountUnlocked,

        RefreshTokenRevoked,

        UserDeleted
    }
    public class UserSecurityEvent
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = default!;

        public SecurityEventType EventType { get; set; }

        public string? Details { get; set; }

        public string? IpAddress { get; set; }

        public DateTime OccurredAtUtc { get; set; } = DateTime.UtcNow;
    }
}
