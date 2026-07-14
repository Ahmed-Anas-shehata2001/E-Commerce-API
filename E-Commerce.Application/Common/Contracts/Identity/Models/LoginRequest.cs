using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common.Contracts.Identity.Models
{
    public class LoginRequest
    {
        public string Email { get; init; } = default!;
        public string Password { get; init; } = default!;
        public bool RememberMe { get; init; }
    }
}
