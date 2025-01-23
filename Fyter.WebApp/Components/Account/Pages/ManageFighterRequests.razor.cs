using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Fyter.WebApp.Data;

namespace Fyter.WebApp.Components.Account.Pages;

public partial class ManageFighterRequests : ComponentBase
{
    [Inject]
    public required UserManager<ApplicationUser> UserManager { get; set; }

    private List<ApplicationUser>? usersWithRequests;

    protected override async Task OnInitializedAsync()
    {
        await LoadUsersWithRequests();
    }

    private async Task LoadUsersWithRequests()
    {
        usersWithRequests = await UserManager
            .Users.Include(u => u.RequestedRoles)
            .Where(u => u.RequestedRoles.Any())
            .ToListAsync();
    }

    private async Task ApproveRequest(string userId, string roleName)
    {
        var user = await UserManager
            .Users.Include(u => u.RequestedRoles)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user != null)
        {
            var result = await UserManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                var RequestedRole = user.RequestedRoles.FirstOrDefault(r => r.RoleName == roleName);
                if (RequestedRole != null)
                {
                    user.RequestedRoles.Remove(RequestedRole);
                    await UserManager.UpdateAsync(user);
                }
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(
                        $"Error adding role {roleName} to user {user.UserName}: {error.Description}"
                    );
                }
            }

            await LoadUsersWithRequests();
        }
    }

    private async Task DenyRequest(string userId, string roleName)
    {
        var user = await UserManager
            .Users.Include(u => u.RequestedRoles)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user != null)
        {
            var requestedRole = user.RequestedRoles.FirstOrDefault(r => r.RoleName == roleName);
            if (requestedRole != null)
            {
                user.RequestedRoles.Remove(requestedRole);
                await UserManager.UpdateAsync(user);
            }

            await LoadUsersWithRequests();
        }
    }
}
