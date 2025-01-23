using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Fyter.WebApp.Services.Interfaces;

namespace Fyter.WebApp.Components.Layout.Shared;

public partial class SharedNavLinks : ComponentBase
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IAuthService AuthService { get; set; }

    private string? currentUrl;

    private bool isAuthenticated = false;
    private bool isAdmin = false;
    private bool isDev = false;

    protected override async Task OnInitializedAsync()
    {
        currentUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
        NavigationManager.LocationChanged += OnLocationChanged;

        isAuthenticated = await AuthService.IsUserAuthenticatedAsync();
        isAdmin = await AuthService.IsUserAuthorizedAsync(RoleConstants.Admin);
        isDev = await AuthService.IsUserAuthorizedAsync(RoleConstants.Developer);
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        currentUrl = NavigationManager.ToBaseRelativePath(e.Location);
        StateHasChanged();
    }

    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
    }
}
