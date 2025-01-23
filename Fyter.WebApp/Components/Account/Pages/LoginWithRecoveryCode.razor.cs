using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Fyter.WebApp.Data;

namespace Fyter.WebApp.Components.Account.Pages;

public partial class LoginWithRecoveryCode : ComponentBase
{
    [Inject]
    public SignInManager<ApplicationUser> SignInManager { get; set; }

    [Inject]
    public UserManager<ApplicationUser> UserManager { get; set; }

    [Inject]
    private IdentityRedirectManager RedirectManager { get; set; }

    [Inject]
    public ILogger<LoginWithRecoveryCode> Logger { get; set; }

    private string? message;
    private ApplicationUser user = default!;

    [SupplyParameterFromForm]
    private InputModel Input { get; set; } = new();

    [SupplyParameterFromQuery]
    private string? ReturnUrl { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // Ensure the user has gone through the username & password screen first
        user =
            await SignInManager.GetTwoFactorAuthenticationUserAsync()
            ?? throw new InvalidOperationException(
                "Unable to load two-factor authentication user."
            );
    }

    private async Task OnValidSubmitAsync()
    {
        var recoveryCode = Input.RecoveryCode.Replace(" ", string.Empty);

        var result = await SignInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

        var userId = await UserManager.GetUserIdAsync(user);

        if (result.Succeeded)
        {
            Logger.LogInformation(
                "User with ID '{UserId}' logged in with a recovery code.",
                userId
            );
            RedirectManager.RedirectTo(ReturnUrl);
        }
        else if (result.IsLockedOut)
        {
            Logger.LogWarning("User account locked out.");
            RedirectManager.RedirectTo("Account/Lockout");
        }
        else
        {
            Logger.LogWarning("Invalid recovery code entered for user with ID '{UserId}' ", userId);
            message = "Error: Invalid recovery code entered.";
        }
    }

    private sealed class InputModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; } = "";
    }
}
