using Microsoft.AspNetCore.Components;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.WebApp.Components.Controls.FiltersComponents;

public partial class FightersFullFilterComponent : ComponentBase
{
    [Inject]
    public required IFighterFilterRepository FighterFilterRepository { get; set; }

    [Parameter]
    public EventCallback RefreshGrid { get; set; }

    private bool IsStandUpFilterApplied => FighterFilterRepository.IsStandUpFilterApplied();
    private bool IsGrapplingFilterApplied => FighterFilterRepository.IsGrapplingFilterApplied();
    private bool IsHealthFilterApplied => FighterFilterRepository.IsHealthFilterApplied();

    private async Task HandleRefreshGrid()
    {
        await RefreshGrid.InvokeAsync();
    }

    private async Task ResetStandUpFilters()
    {
        await FighterFilterRepository.ResetStandUpFilters();
        await HandleRefreshGrid();
        StateHasChanged();

        await RefreshGrid.InvokeAsync();
    }

    private async Task ResetGrapplingFilters()
    {
        await FighterFilterRepository.ResetGrapplingFilters();
        await HandleRefreshGrid();
        StateHasChanged();

        await RefreshGrid.InvokeAsync();
    }

    private async Task ResetHealthFilters()
    {
        await FighterFilterRepository.ResetHealthFilters();
        await HandleRefreshGrid();
        StateHasChanged();
    }
}
