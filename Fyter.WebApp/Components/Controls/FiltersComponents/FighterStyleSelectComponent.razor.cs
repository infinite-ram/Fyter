using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Filters.Interfaces;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.WebApp.Components.Controls.FiltersComponents;

public partial class FighterStyleSelectComponent : ComponentBase
{
    [Inject]
    public required IFighterFilterRepository FighterFilterRepository { get; set; }

    [Inject]
    public required ISetFighterStyleFilterUseCase SetFighterStyleFilterUseCase { get; set; }

    [Inject]
    public required IResetFilterQueryUseCase ResetFilterQueryUseCase { get; set; }

    public FighterStyleEnum? selectedFighterStyle { get; set; }

    [Parameter]
    public EventCallback RefreshGrid { get; set; }

    protected override void OnInitialized()
    {
        FighterFilterRepository.OnFilterChanged += HandleFilterChanged;
    }

    private void HandleFilterChanged()
    {
        selectedFighterStyle = FighterFilterRepository.SelectedFighterStyle;
        InvokeAsync(StateHasChanged);
    }

    private string GetButtonLabel()
    {
        return selectedFighterStyle.HasValue
            ? selectedFighterStyle.Value.ToString()
            : "Fighter Style";
    }

    private async Task Reset()
    {
        selectedFighterStyle = null;
        await SetFighterStyleFilterUseCase.ExecuteAsync(selectedFighterStyle);
        await OnRefreshGrid();
    }

    private async Task OnSelectFighterStyle(FighterStyleEnum? style)
    {
        selectedFighterStyle = style;
        await SetFighterStyleFilterUseCase.ExecuteAsync(selectedFighterStyle);
        await OnRefreshGrid();
        StateHasChanged();
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
