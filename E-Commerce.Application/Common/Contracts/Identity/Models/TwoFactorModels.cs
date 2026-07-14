using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common.Contracts.Identity.Models
{

    public class TwoFactorModels
    {
        // NOte)  I'm using CQRS  , so i'll map them in handlers

        public sealed class TwoFactorSetupResult
        {
            public string SharedKey { get; init; } = default!;
            public string AuthenticatorUri { get; init; } = default!;
        }

        public sealed class TwoFactorVerifyRequest
        {
            public string UserId { get; init; } = default!;
            public string Code { get; init; } = default!;
        }

        public sealed class TwoFactorLoginRequest
        {
            public string UserId { get; init; } = default!;
            public string Code { get; init; } = default!;
            public bool RememberMachine { get; init; }
        }
    }
}
