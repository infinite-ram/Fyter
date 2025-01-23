using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Fyter.WebApp.Components.Account.Pages.Models;
using Fyter.WebApp.Data;

namespace Fyter.WebApp.Components.Account.Pages;

public partial class ConfirmEmail : ComponentBase
{
    [Inject]
    public IDataProtectionProvider DataProtectionProvider { get; set; }

    [Inject]
    public SignInManager<ApplicationUser> SignInManager { get; set; }

    [Inject]
    public UserManager<ApplicationUser> UserManager { get; set; }

    [Inject]
    private IdentityRedirectManager RedirectManager { get; set; }

    private string? statusMessage;

    [CascadingParameter]
    private HttpContext HttpContext { get; set; } = default!;

    [SupplyParameterFromQuery]
    private string? ReturnUrl { get; set; }

    [SupplyParameterFromQuery]
    private string? Token { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Token is null)
        {
            RedirectManager.RedirectTo("");
            return;
        }

        var protector = DataProtectionProvider
            .CreateProtector("UserRegistration")
            .ToTimeLimitedDataProtector();

        string payload;
        try
        {
            payload = protector.Unprotect(Token, out DateTimeOffset expiration);
            if (expiration < DateTimeOffset.UtcNow)
            {
                statusMessage = "Token has expired";
            }
        }
        catch (System.Exception)
        {
            statusMessage = "Invalid or expired token.";
            return;
        }

        var userData = JsonSerializer.Deserialize<UserData>(payload);

        if (userData == null)
        {
            statusMessage = "Invalid token data.";
            return;
        }

        var user = new ApplicationUser { UserName = userData.UserName, Email = userData.Email };

        var result = await UserManager.CreateAsync(user, userData.Password);
        if (!result.Succeeded)
        {
            statusMessage =
                $"Error creating user: {string.Join(", ", result.Errors.Select(e => e.Description))}";
            return;
        }

        user.EmailConfirmed = true;
        await UserManager.UpdateAsync(user);
        await UserManager.AddToRoleAsync(user, RoleConstants.Viewer);

        statusMessage = "Thank you for confirming your email.";
        await SignInManager.SignInAsync(user, isPersistent: false);

        RedirectManager.RedirectTo(ReturnUrl);
    }
}
