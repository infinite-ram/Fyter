using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;

namespace Fyter.WebApp.Components.Controls.FiltersComponents;

public partial class FightersCategoryFilterComponent : ComponentBase
{
    [Parameter]
    public EventCallback<DivisionEnum?> OnSelectedDivision { get; set; }
    private DivisionEnum? selectedDivision { get; set; }

    [Parameter]
    public EventCallback<PerksEnum?> OnSelectedPerk { get; set; }
    private PerksEnum? selectedPerk { get; set; }

    [Parameter]
    public EventCallback RefreshGrid { get; set; }

    private async Task HandleSelectedDivision(DivisionEnum? division)
    {
        selectedDivision = division;
        await OnSelectedDivision.InvokeAsync(selectedDivision);
        await RefreshGrid.InvokeAsync();
    }

    private async Task HandleSelectedPerk(PerksEnum? perk)
    {
        selectedPerk = perk;
        await OnSelectedPerk.InvokeAsync(selectedPerk);
        await RefreshGrid.InvokeAsync();
    }

    private async Task HandleRefreshGrid()
    {
        await RefreshGrid.InvokeAsync();
    }
}
