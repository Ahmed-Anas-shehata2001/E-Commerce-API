using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Identity.Identity_Entites
{

    public enum TwoFactorMethodType
    {
        Email = 1,

        Sms,

        Authenticator
    }
    public class UserTwoFactorMethod
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = default!;

        public TwoFactorMethodType MethodType { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime EnabledAtUtc { get; set; }
    }
}
