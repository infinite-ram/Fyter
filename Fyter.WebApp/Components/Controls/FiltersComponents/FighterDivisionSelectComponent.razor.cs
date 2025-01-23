using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Filters.Interfaces;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.WebApp.Components.Controls.FiltersComponents;

public partial class FighterDivisionSelectComponent : ComponentBase
{
    [Inject]
    public required IFighterFilterRepository FighterFilterRepository { get; set; }

    [Inject]
    public required ISetFighterDivisionFilterUseCase SetFighterDivisionFilterUseCase { get; set; }

    [Inject]
    public required IResetFilterQueryUseCase ResetFilterQueryUseCase { get; set; }

    [Parameter]
    public DivisionEnum? selectedDivision { get; set; }

    [Parameter]
    public EventCallback RefreshGrid { get; set; }

    protected override void OnInitialized()
    {
        FighterFilterRepository.OnFilterChanged += HandleFilterChanged;
    }

    private void HandleFilterChanged()
    {
        selectedDivision = FighterFilterRepository.SelectedDivision;
        InvokeAsync(StateHasChanged);
    }

    private string GetButtonLabel()
    {
        return selectedDivision.HasValue ? selectedDivision.Value.ToString() : "Divisions";
    }

    private async Task Reset()
    {
        selectedDivision = null;
        await SetFighterDivisionFilterUseCase.ExecuteAsync(selectedDivision);
        await OnRefreshGrid();
    }

    private async Task OnSelectDivision(DivisionEnum? division)
    {
        selectedDivision = division;
        await SetFighterDivisionFilterUseCase.ExecuteAsync(selectedDivision);
        await OnRefreshGrid();
    }

    private async Task OnRefreshGrid()
    {
        await RefreshGrid.InvokeAsync();
    }

    public void Dispose()
    {
        FighterFilterRepository.OnFilterChanged -= HandleFilterChanged;
    }
}
