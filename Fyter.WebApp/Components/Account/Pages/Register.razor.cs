using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Fyter.WebApp.Components.Account.Pages.Models;
using Fyter.WebApp.Data;

namespace Fyter.WebApp.Components.Account.Pages;

public partial class Register : ComponentBase
{
    [Inject]
    public UserManager<ApplicationUser> UserManager { get; set; }

    [Inject]
    public IUserStore<ApplicationUser> UserStore { get; set; }

    [Inject]
    public SignInManager<ApplicationUser> SignInManager { get; set; }

    [Inject]
    public IEmailSender<ApplicationUser> EmailSender { get; set; }

    [Inject]
    public ILogger<Register> Logger { get; set; }

    [Inject]
    public IDataProtectionProvider DataProtectionProvider { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    private IdentityRedirectManager RedirectManager { get; set; }

    private IEnumerable<IdentityError>? identityErrors;

    [SupplyParameterFromForm]
    private InputModel Input { get; set; } = new();

    [SupplyParameterFromQuery]
    private string? ReturnUrl { get; set; }

    private string? Message =>
        identityErrors is null
            ? null
            : $"Error: {string.Join(", ", identityErrors.Select(error => error.Description))}";

    public async Task RegisterUser(EditContext editContext)
    {
        var existingUserWithUsername = await UserManager.FindByNameAsync(Input.UserName);
        if (existingUserWithUsername != null)
        {
            identityErrors = new List<IdentityError>
            {
                new IdentityError { Description = "Username is already taken." },
            };
            return;
        }

        // Check if email is already taken
        var existingUserWithEmail = await UserManager.FindByEmailAsync(Input.Email);
        if (existingUserWithEmail != null)
        {
            identityErrors = new List<IdentityError>
            {
                new IdentityError { Description = "Email is already taken." },
            };
            return;
        }

        var userData = new UserData
        {
            Email = Input.Email,
            UserName = Input.UserName,
            Password = Input.Password,
        };

        var payload = JsonSerializer.Serialize(userData);

        var protector = DataProtectionProvider
            .CreateProtector("UserRegistration")
            .ToTimeLimitedDataProtector();
        var token = protector.Protect(payload, TimeSpan.FromDays(1));

        var callbackUrl = NavigationManager.GetUriWithQueryParameters(
            NavigationManager.ToAbsoluteUri("Account/ConfirmEmail").AbsoluteUri,
            new Dictionary<string, object?> { ["token"] = token, ["returnUrl"] = ReturnUrl }
        );

        var tempUser = new ApplicationUser { UserName = Input.UserName };

        await EmailSender.SendConfirmationLinkAsync(
            tempUser,
            Input.Email,
            HtmlEncoder.Default.Encode(callbackUrl)
        );

        RedirectManager.RedirectTo(
            "Account/RegisterConfirmation",
            new() { ["email"] = Input.Email, ["returnUrl"] = ReturnUrl }
        );
    }

    private ApplicationUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<ApplicationUser>();
        }
        catch
        {
            throw new InvalidOperationException(
                $"Can't create an instance of '{nameof(ApplicationUser)}'. "
                    + $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor."
            );
        }
    }

    private IUserEmailStore<ApplicationUser> GetEmailStore()
    {
        if (!UserManager.SupportsUserEmail)
        {
            throw new NotSupportedException(
                "The default UI requires a user store with email support."
            );
        }
        return (IUserEmailStore<ApplicationUser>)UserStore;
    }

    private sealed class InputModel
    {
        [Required]
        [StringLength(
            20,
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 3
        )]
        [RegularExpression(
            @"^\w+$",
            ErrorMessage = "The username can only contain letters, numbers, and underscores."
        )]
        [Display(Name = "Username")]
        public string UserName { get; set; } = "";

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = "";

        [Required]
        [StringLength(
            100,
            ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6
        )]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = "";

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = "";
    }
}
