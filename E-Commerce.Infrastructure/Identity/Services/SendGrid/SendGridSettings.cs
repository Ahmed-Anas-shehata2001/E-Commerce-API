using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Identity.Services.SendGrid
{
    public class SendGridSettings
    {
        public string ApiKey { get; set; } = string.Empty; // came from user secrets  ( Development )
        public string FromEmail { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
    }
}
