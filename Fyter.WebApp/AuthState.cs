namespace Fyter.WebApp;

public class AuthState
{
    public bool IsUserAuthenticated { get; set; } = false;
    public bool IsUserAuthorized { get; set; } = false;
}
