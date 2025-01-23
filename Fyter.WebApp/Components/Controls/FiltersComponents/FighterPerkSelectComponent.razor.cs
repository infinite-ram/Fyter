using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Filters.Interfaces;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.WebApp.Components.Controls.FiltersComponents;

public partial class FighterPerkSelectComponent : ComponentBase
{
    [Inject]
    public required IFighterFilterRepository FighterFilterRepository { get; set; }

    [Inject]
    public required ISetFighterPerkFilterUseCase SetFighterPerkFilterUseCase { get; set; }

    [Inject]
    public required IResetFilterQueryUseCase ResetFilterQueryUseCase { get; set; }

    [Parameter]
    public PerksEnum? selectedPerk { get; set; }

    [Parameter]
    public EventCallback RefreshGrid { get; set; }

    protected override void OnInitialized()
    {
        FighterFilterRepository.OnFilterChanged += HandleFilterChanged;
    }

    private void HandleFilterChanged()
    {
        selectedPerk = FighterFilterRepository.SelectedPerk;
        InvokeAsync(StateHasChanged);
    }

    private string GetButtonLabel()
    {
        return selectedPerk.HasValue ? selectedPerk.Value.ToString() : "Select Perk";
    }

    private async Task Reset()
    {
        selectedPerk = null;
        await SetFighterPerkFilterUseCase.ExecuteAsync(selectedPerk);
        await OnRefreshGrid();
    }

    private async Task OnSelectFighterPerk(PerksEnum? perk)
    {
        selectedPerk = perk;
        await SetFighterPerkFilterUseCase.ExecuteAsync(selectedPerk);
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
