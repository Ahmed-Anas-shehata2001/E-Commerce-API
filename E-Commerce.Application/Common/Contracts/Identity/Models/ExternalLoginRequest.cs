using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common.Contracts.Identity.Models
{
    public class ExternalLoginRequest
    {
        // e.g. "Google", "Facebook", "Microsoft"
        public string Provider { get; init; } = default!;
        // Id token / access token issued by the external provider, verified server-side
        public string ProviderToken { get; init; } = default!;
    }
}
