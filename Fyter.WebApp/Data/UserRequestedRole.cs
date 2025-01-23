namespace Fyter.WebApp.Data;

public class UserRequestedRole
{
    public int Id { get; set; }
    public string RoleName { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}
