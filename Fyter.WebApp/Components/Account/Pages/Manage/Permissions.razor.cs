using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Fyter.WebApp.Data;

namespace Fyter.WebApp.Components.Account.Pages.Manage;

public partial class Permissions : ComponentBase
{
    [Inject]
    public required UserManager<ApplicationUser> UserManager { get; set; }

    [Inject]
    public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    public InputModel Input { get; set; } = new();

    private ApplicationUser? user;
    private bool isAuthenticated = false;
    private bool isAuthorized = false;

    private IList<string> userRoles = new List<string>();
    private List<string> availableRoles = RoleConstants
        .AllRoles.Where(r => r != RoleConstants.Admin)
        .ToList();

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var userClaim = authState.User;
        var userId = UserManager.GetUserId(userClaim);
        user = await UserManager
            .Users.Include(r => r.RequestedRoles)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            NavigationManager.NavigateTo("/Account/Login");

        isAuthenticated = userClaim.Identity?.IsAuthenticated ?? false;
        if (isAuthenticated)
            userRoles = await UserManager.GetRolesAsync(user);

        var requestedRoleNames = user.RequestedRoles.Select(rr => rr.RoleName).ToList();

        Input.RoleSelections = availableRoles
            .Select(role => new RoleSelection
            {
                RoleName = role,
                IsSelected = userRoles.Contains(role) || requestedRoleNames.Contains(role),
            })
            .ToList();
    }

    private async Task RequestPermission()
    {
        var newRequestedRoles = Input
            .RoleSelections.Where(r =>
                r.IsSelected
                && !userRoles.Contains(r.RoleName)
                && !user.RequestedRoles.Any(rr => rr.RoleName == r.RoleName)
            )
            .Select(r => r.RoleName)
            .ToList();

        if (newRequestedRoles.Any())
        {
            foreach (var role in newRequestedRoles)
            {
                var roleRequest = new UserRequestedRole { RoleName = role, UserId = user.Id };
                user.RequestedRoles.Add(roleRequest);
            }

            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                }
            }
        }
        else
        {
            NavigationManager.Refresh();
        }
    }

    private async Task RevokeRole(string role)
    {
        var requestedRoleNames = user.RequestedRoles.ToList();
        var roleToRevoke = requestedRoleNames.FirstOrDefault(r => r.RoleName == role);
        if (roleToRevoke != null)
        {
            user.RequestedRoles.Remove(roleToRevoke);
        }

        var result = await UserManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error: {error.Description}");
            }
        }

        var input = Input.RoleSelections.FirstOrDefault(r => r.RoleName == role);
        if (input != null)
            input.IsSelected = false;
    }

    public class InputModel
    {
        public bool IsTesting { get; set; }
        public List<RoleSelection> RoleSelections { get; set; } = new();
    }

    public class RoleSelection
    {
        public string RoleName { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }
}
