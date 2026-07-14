using E_Commerce.Application.Common.Contracts.Identity;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail; 

namespace E_Commerce.Infrastructure.Identity.Services.SendGrid;

public class SendGridEmailService : IEmailSender
{
    private readonly SendGridSettings _settings;
    private readonly ISendGridClient _client;
    public SendGridEmailService(IOptions<SendGridSettings> options , ISendGridClient client)
    {
        _settings = options.Value;
        _client = client;
    }

    public async Task SendAsync(
        string toEmail,
        string subject,
        string htmlBody,
        CancellationToken ct = default)
    {

        var from = new EmailAddress(
            _settings.FromEmail,
            _settings.FromName);

        var to = new EmailAddress(toEmail);

        var message = MailHelper.CreateSingleEmail(
            from,
            to,
            subject,
            plainTextContent: null,
            htmlContent: htmlBody);

        var response = await _client.SendEmailAsync(message, ct);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Body.ReadAsStringAsync(ct);

            throw new Exception(
                $"Failed to send email. Status: {response.StatusCode}, Body: {body}");
        }
    }
}

// ********************************  Fake implementation **************************************8 // 
//using E_Commerce.Application.Common.Contracts.Identity;
//using E_Commerce.Infrastructure.Identity.Services.SendGrid;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using SendGrid;
//using SendGrid.Helpers.Mail;

//namespace E_Commerce.Infrastructure.Identity.Services
//{
//    //  *******  I'll do this later ********  // 
//    //public class EmailSender : IEmailSender
//    //{
//    //    public async Task SendAsync(
//    //    string toEmail,
//    //    string subject,
//    //    string htmlBody,
//    //    CancellationToken ct = default)
//    //    {
//    //        // SMTP / MailKit / SendGrid implementation
//    //    }
//    //}

//    // fake email sender now


//    public class FakeEmailSender : IEmailSender
//    {
//        private readonly ILogger<FakeEmailSender> _logger;

//        public FakeEmailSender(ILogger<FakeEmailSender> logger)
//        {
//            _logger = logger;
//        }

//        public Task SendAsync(
//       string toEmail,
//       string subject,
//       string htmlBody,
//       CancellationToken ct = default)
//        {
//            _logger.LogInformation("""
//        ---------------- EMAIL ----------------
//        To: {To}
//        Subject: {Subject}

//        {Body}

//        ---------------------------------------
//        """,
//                toEmail,
//                subject,
//                htmlBody);

//            return Task.CompletedTask;
//        }
//    }
//}
