using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Fyter.WebApp.Data;

namespace Fyter.WebApp.Components.Account.Pages.Manage;

public partial class ResetAuthenticator : ComponentBase
{
    [Inject]
    public UserManager<ApplicationUser> UserManager { get; set; }

    [Inject]
    public SignInManager<ApplicationUser> SignInManager { get; set; }

    [Inject]
    private IdentityUserAccessor UserAccessor { get; set; }

    [Inject]
    private IdentityRedirectManager RedirectManager { get; set; }

    [Inject]
    public ILogger<ResetAuthenticator> Logger { get; set; }

    [CascadingParameter]
    private HttpContext HttpContext { get; set; } = default!;

    private async Task OnSubmitAsync()
    {
        var user = await UserAccessor.GetRequiredUserAsync(HttpContext);
        await UserManager.SetTwoFactorEnabledAsync(user, false);
        await UserManager.ResetAuthenticatorKeyAsync(user);
        var userId = await UserManager.GetUserIdAsync(user);
        Logger.LogInformation(
            "User with ID '{UserId}' has reset their authentication app key.",
            userId
        );

        await SignInManager.RefreshSignInAsync(user);

        RedirectManager.RedirectToWithStatus(
            "Account/Manage/EnableAuthenticator",
            "Your authenticator app key has been reset, you will need to configure your authenticator app using the new key.",
            HttpContext
        );
    }
}
