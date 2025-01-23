using Microsoft.AspNetCore.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using Fyter.WebApp.Data;

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
        var apiKey = _configuration["SendGrid:ApiKey"];
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("SendGrid API key is not configured.");
        }

        var client = new SendGridClient(apiKey);

        var fromEmail = new EmailAddress(
            _configuration["SendGrid:FromEmail"],
            _configuration["SendGrid:FromName"]
        );
        var toEmail = new EmailAddress(email, userName);

        var msg = MailHelper.CreateSingleEmail(
            fromEmail,
            toEmail,
            subject,
            htmlContent,
            htmlContent
        );

        var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

        if (
            response.StatusCode != System.Net.HttpStatusCode.OK
            && response.StatusCode != System.Net.HttpStatusCode.Accepted
        )
        {
            throw new InvalidOperationException(
                $"Failed to send email. Status Code: {response.StatusCode}"
            );
        }
    }
}
