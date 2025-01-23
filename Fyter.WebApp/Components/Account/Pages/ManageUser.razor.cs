using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Fyter.WebApp.Data;
using Fyter.WebApp.Services;

namespace Fyter.WebApp.Components.Account.Pages;

public partial class ManageUser : ComponentBase
{
    [Inject]
    public required UserManager<ApplicationUser> UserManager { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required LoadingService LoadingService { get; set; }

    [Parameter]
    public string? UserId { get; set; }

    private ApplicationUser? user;

    private ManageUserViewModel? viewModel { get; set; }

    private List<string> AllRoles => RoleConstants.AllRoles;

    protected override async Task OnParametersSetAsync()
    {
        if (string.IsNullOrEmpty(UserId))
        {
            NavigationManager.NavigateTo("/account/manageusers");
            return;
        }

        // user = await UserService.GetUserByIdAsync(UserId);
        user = await UserManager
            .Users.Include(u => u.RequestedRoles)
            .FirstOrDefaultAsync(u => u.Id == UserId);

        if (user == null)
        {
            NavigationManager.NavigateTo("/account/manageusers");
            return;
        }

        var userRoles = await UserManager.GetRolesAsync(user);
        var requestedRoleNames = user.RequestedRoles.Select(rr => rr.RoleName).ToList();

        viewModel = new ManageUserViewModel
        {
            Email = user.Email,
            UserName = user.UserName,
            Roles = AllRoles
                .Select(role => new RoleSelection
                {
                    RoleName = role,
                    IsSelected = userRoles.Contains(role),
                    IsRequested = requestedRoleNames.Contains(role),
                })
                .ToList(),
        };
    }

    private async Task SaveUser()
    {
        if (user == null || viewModel == null)
            return;

        if (LoadingService != null)
        {
            LoadingService.IsVisible = true;
            LoadingService.Refresh();

            LoadingService.IsVisible = false;
            LoadingService.Refresh();
        }

        var selectedRoles = viewModel
            .Roles.Where(r => r.IsSelected)
            .Select(r => r.RoleName)
            .ToList();

        var currentRoles = await UserManager.GetRolesAsync(user);

        var rolesToAdd = selectedRoles.Except(currentRoles).ToList();
        var rolesToRemove = currentRoles.Except(selectedRoles).ToList();

        foreach (var role in rolesToAdd)
        {
            var result = await UserManager.AddToRoleAsync(user, role);
            if (result.Succeeded)
            {
                var requestedRole = user.RequestedRoles.FirstOrDefault(rr => rr.RoleName == role);
                if (requestedRole != null)
                    user.RequestedRoles.Remove(requestedRole);
            }
        }

        foreach (var role in rolesToRemove)
        {
            var result = await UserManager.RemoveFromRoleAsync(user, role);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(
                        $"Error removing role {role} from user {user.UserName}: {error.Description}"
                    );
                }
            }
        }

        await UserManager.UpdateAsync(user);
        NavigationManager.Refresh();
    }

    public class ManageUserViewModel
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }

        public List<RoleSelection> Roles { get; set; } = new();
    }

    public class RoleSelection
    {
        public string RoleName { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
        public bool IsRequested { get; set; }
    }
}
