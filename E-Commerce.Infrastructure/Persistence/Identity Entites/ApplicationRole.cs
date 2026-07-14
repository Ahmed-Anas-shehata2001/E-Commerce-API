using E_Commerce.Domain.Common.Base;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Infrastructure.Identity
{
    public class ApplicationRole :  IdentityRole<Guid>
    {
        public string? Description { get; set; }

    }
}
