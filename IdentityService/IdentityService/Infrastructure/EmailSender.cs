using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;
using ILogger = Serilog.ILogger;

namespace IdentityService.Infrastructure;

public class EmailSender: IEmailSender
{
    private readonly ILogger _logger;

    public EmailSender(ILogger logger)
    {
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        //await Task.Run(() => _logger.Information("Email sent!\nEmail: {}\nSubject: {}\nHTML Message: {}", email, subject, htmlMessage));
        await Task.Run(() => _logger.Information(
            "Email sent!\nEmail: {Email}\nSubject: {Subject}\nHTML Message: {HtmlMessage}",
            email,
            subject,
            htmlMessage));
    }
}