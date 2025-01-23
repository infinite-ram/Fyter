using Microsoft.AspNetCore.Identity;

namespace Fyter.WebApp.Data;

public class ApplicationUser : IdentityUser
{
    public List<UserRequestedRole> RequestedRoles { get; set; } = new List<UserRequestedRole>();
}
