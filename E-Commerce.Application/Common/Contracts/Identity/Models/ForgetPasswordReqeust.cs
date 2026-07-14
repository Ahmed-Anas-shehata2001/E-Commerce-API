using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Common.Contracts.Identity.Models
{
    public class ForgetPasswordReqeust
    {
        public string Email { get; init; } = default!;

    }
}
