using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Identity.Identity_Entites
{
    public class UserLoginHistory
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = default!;

        public DateTime LoginAtUtc { get; set; }

        public bool Succeeded { get; set; }

        public string? FailureReason { get; set; }

        public string? IpAddress { get; set; }

        public string? UserAgent { get; set; }
    }
}
