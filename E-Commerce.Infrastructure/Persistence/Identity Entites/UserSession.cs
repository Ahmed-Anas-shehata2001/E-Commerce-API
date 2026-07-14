using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Identity.Identity_Entites
{
    public class UserSession
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = default!;

        public string DeviceName { get; set; } = string.Empty;

        public string? UserAgent { get; set; }

        public string? IpAddress { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime LastActivityAtUtc { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime? RevokedAtUtc { get; set; }
        public bool IsActive => !IsRevoked;

        public string? OperatingSystem { get; set; }

        public string? Browser { get; set; }

        public ICollection<RefreshTokenEntity> RefreshTokens { get; set; } = [];
    }
}
