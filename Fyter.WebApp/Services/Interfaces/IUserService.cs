using Microsoft.AspNetCore.Identity;
using Fyter.WebApp.Data;

namespace Fyter.WebApp.Services.Interfaces;

public interface IUserService
{
    Task<ApplicationUser> GetUserByIdAsync(string userId);
    Task<List<string>> GetUserRolesAsync(ApplicationUser user);
    Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role);
    Task<IdentityResult> RemoveFromRoleAsync(ApplicationUser user, string role);
}
