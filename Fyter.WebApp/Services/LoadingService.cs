namespace Fyter.WebApp.Services;

public class LoadingService
{
    public event Action? OnRefresh;
    public bool IsVisible { get; set; } = false;

    public void Refresh()
    {
        OnRefresh?.Invoke();
    }
}
