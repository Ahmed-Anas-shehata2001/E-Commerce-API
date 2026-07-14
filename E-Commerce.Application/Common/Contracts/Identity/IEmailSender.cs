namespace E_Commerce.Application.Common.Contracts.Identity
{
    // I'll use SendGrid  as an email provider   
    /*
     * confirmation email
     * reset password
     * 2fA code
     * shipping updates , etc
     */
    public interface IEmailSender
    {
      
        Task SendAsync(string toEmail, string subject, string htmlBody, CancellationToken ct = default);
    }
}
