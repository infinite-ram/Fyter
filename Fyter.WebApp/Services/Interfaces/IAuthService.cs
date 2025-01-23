namespace Fyter.WebApp.Services.Interfaces;

public interface IAuthService
{
    Task<bool> IsUserAuthenticatedAsync();
    Task<bool> IsUserAuthorizedAsync(params string[] roles);
    Task<string> GetUserIdAsync();
}
