using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Fyter.WebApp.Data;

namespace Fyter.WebApp.Components.Account.Pages;

public partial class ConfirmEmailChange : ComponentBase
{
    [Inject]
    public UserManager<ApplicationUser> UserManager { get; set; }

    [Inject]
    public SignInManager<ApplicationUser> SignInManager { get; set; }

    [Inject]
    private IdentityRedirectManager RedirectManager { get; set; }
    private string? message;

    [CascadingParameter]
    private HttpContext HttpContext { get; set; } = default!;

    [SupplyParameterFromQuery]
    private string? UserId { get; set; }

    [SupplyParameterFromQuery]
    private string? Email { get; set; }

    [SupplyParameterFromQuery]
    private string? Code { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (UserId is null || Email is null || Code is null)
        {
            RedirectManager.RedirectToWithStatus(
                "Account/Login",
                "Error: Invalid email change confirmation link.",
                HttpContext
            );
        }

        var user = await UserManager.FindByIdAsync(UserId);
        if (user is null)
        {
            message = "Unable to find user with Id '{userId}'";
            return;
        }

        var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Code));
        var result = await UserManager.ChangeEmailAsync(user, Email, code);
        if (!result.Succeeded)
        {
            message = "Error changing email.";
            return;
        }

        // In our UI email and user name are one and the same, so when we update the email
        // we need to update the user name.
        var setUserNameResult = await UserManager.SetUserNameAsync(user, Email);
        if (!setUserNameResult.Succeeded)
        {
            message = "Error changing user name.";
            return;
        }

        await SignInManager.RefreshSignInAsync(user);
        message = "Thank you for confirming your email change.";
    }
}
