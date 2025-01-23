using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;
using Fyter.WebApp.Services.Interfaces;

namespace Fyter.WebApp.Components.Controls.FighterDetailComponents;

public partial class FighterGeneralInfoComponent : ComponentBase
{
    [Inject]
    public required IAuthService AuthService { get; set; }

    [Inject]
    public required IUserService UserService { get; set; }

    [Parameter]
    public required Fighter Fighter { get; set; }

    private bool isAuthenticated = false;
    private bool isAuthorized = false;

    public string AddedByUserName { get; set; } = "Unknown";
    public string LastUpdatedByUserName { get; set; } = null;

    protected override async Task OnInitializedAsync()
    {
        isAuthenticated = await AuthService.IsUserAuthenticatedAsync();
        isAuthorized = await AuthService.IsUserAuthorizedAsync(RoleConstants.Admin);

        if (!string.IsNullOrEmpty(Fighter.AddedByUserId))
        {
            var user = await UserService.GetUserByIdAsync(Fighter.AddedByUserId);
            AddedByUserName = user?.UserName ?? "Unknown";
        }

        if (!string.IsNullOrEmpty(Fighter.LastUpdatedByUserId))
        {
            var user = await UserService.GetUserByIdAsync(Fighter.LastUpdatedByUserId);
            LastUpdatedByUserName = user?.UserName ?? null;
        }
    }
}
