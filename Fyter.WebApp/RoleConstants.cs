namespace Fyter.WebApp;

public static class RoleConstants
{
    public const string Role = "Role";

    public const string Admin = "Admin";
    public const string FighterEditor = "FighterEditor";
    public const string Viewer = "Viewer";
    public const string Developer = "Developer";

    public static readonly List<string> AllRoles = new List<string>()
    {
        Admin,
        FighterEditor,
        Viewer,
        Developer,
    };
}
