using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common.Contracts.Identity.Models
{
    public class RegisterRequest
    {
        public string FirstName { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string Password { get; init; } = default!;
        public string ConfirmPassword { get; init; } = default!;
        public string? PhoneNumber { get; init; }
    }
}
