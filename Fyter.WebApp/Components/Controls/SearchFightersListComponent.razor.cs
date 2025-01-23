using Microsoft.AspNetCore.Components;

namespace Fyter.WebApp.Components.Controls;

public partial class SearchFightersListComponent : ComponentBase
{
    [Parameter]
    public int TotalResults { get; set; }

    private string? nameFilter;

    [Parameter]
    public EventCallback<string> OnSearchInput { get; set; }

    [Parameter]
    public string? Title { get; set; }

    [Parameter]
    public string? SubTitle { get; set; }

    private async Task InvokeOnSearchInput()
    {
        await OnSearchInput.InvokeAsync(nameFilter);
    }
}
