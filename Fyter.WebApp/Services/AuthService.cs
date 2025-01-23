using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Fyter.WebApp.Services.Interfaces;

namespace Fyter.WebApp.Services;

public class AuthService : IAuthService
{
    private ClaimsPrincipal? user;
    private readonly AuthenticationStateProvider authenticationStateProvider;

    public AuthService(AuthenticationStateProvider authenticationStateProvider)
    {
        this.authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> IsUserAuthenticatedAsync()
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        return authState.User?.Identity?.IsAuthenticated ?? false;
    }

    public async Task<bool> IsUserAuthorizedAsync(params string[] roles)
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user.Identity?.IsAuthenticated == true && roles.Any(role => user.IsInRole(role));
    }

    public async Task<string> GetUserIdAsync()
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public Task SignOutAsync()
    {
        throw new NotImplementedException();
    }
}
