using Microsoft.AspNetCore.Identity;
using Fyter.WebApp.Data;
using System.Net.Mail;
using System.Net;

namespace Fyter.WebApp.Components.Account;

internal sealed class IdentityEmailSender : IEmailSender<ApplicationUser>
{
    private readonly IConfiguration _configuration;

    public IdentityEmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task SendConfirmationLinkAsync(
        ApplicationUser user,
        string email,
        string confirmationLink
    )
    {
        var subject = "Confirm your email";
        var htmlContent =
            $"<p>Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.</p>";
        return SendEmailAsync(email, user.UserName, subject, htmlContent);
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        var subject = "Reset your password";
        var htmlContent =
            $"<p>Please reset your password by <a href='{resetLink}'>clicking here</a>.</p>";
        return SendEmailAsync(email, user.UserName, subject, htmlContent);
    }

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        var subject = "Reset your password";
        var htmlContent =
            $"<p>Please reset your password using the following code: <strong>{resetCode}</strong></p>";
        return SendEmailAsync(email, user.UserName, subject, htmlContent);
    }

    private async Task SendEmailAsync(
        string email,
        string userName,
        string subject,
        string htmlContent
    )
    {
        // Pull SMTP settings from appsettings.json
        var host = _configuration["Smtp:Host"];
        var port = int.Parse(_configuration["Smtp:Port"] ?? "587");
        var smtpUser = _configuration["Smtp:Username"];
        var smtpPass = _configuration["Smtp:Password"];
        var fromEmail = _configuration["Smtp:FromEmail"];
        var fromName = _configuration["Smtp:FromName"];

        if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(smtpUser) || string.IsNullOrEmpty(smtpPass))
        {
            throw new InvalidOperationException("SMTP settings are not configured properly.");
        }

        // Create the mail message
        var message = new MailMessage
        {
            From = new MailAddress(fromEmail, fromName),
            Subject = subject,
            Body = htmlContent,
            IsBodyHtml = true
        };
        message.To.Add(new MailAddress(email, userName));

        // Configure the SMTP client
        using var smtpClient = new SmtpClient(host, port)
        {
            Credentials = new NetworkCredential(smtpUser, smtpPass),
            EnableSsl = true
        };

        // Send the email
        await smtpClient.SendMailAsync(message);
    }
}
