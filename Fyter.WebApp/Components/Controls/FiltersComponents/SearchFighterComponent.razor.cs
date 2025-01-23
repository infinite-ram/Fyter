using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;
using Fyter.WebApp.Utilities;

namespace Fyter.WebApp.Components.Controls.FiltersComponents;

public partial class SearchFighterComponent : ComponentBase
{
    [Inject]
    public required IViewBaseFightersUseCase ViewBaseFightersUseCase { get; set; }

    [Inject]
    public required IViewFighterByIdUseCase ViewFighterByIdUseCase { get; set; }

    [Parameter]
    public EventCallback<int?> OnFighterSelected { get; set; }

    [Parameter]
    public Fighter? SelectedFighter { get; set; }

    private string _searchQuery = string.Empty;
    private string searchQuery
    {
        get => _searchQuery;
        set
        {
            if (_searchQuery != value)
            {
                _searchQuery = value;
                SearchFighters();
            }
        }
    }

    private List<Fighter> filteredFighters = new List<Fighter>();

    private string Placeholder = "Search Fighters ...";

    protected override async Task OnInitializedAsync()
    {
        if (SelectedFighter != null)
        {
            filteredFighters.Clear();
            ComputePlaceholder();
        }
        else
        {
            filteredFighters = await ViewBaseFightersUseCase.ExecuteAsync("", 10);
        }
    }

    protected override void OnParametersSet()
    {
        if (SelectedFighter != null)
        {
            filteredFighters.Clear();
            ComputePlaceholder();
            StateHasChanged();
        }
    }

    private async Task SearchFighters()
    {
        if (string.IsNullOrEmpty(searchQuery))
        {
            filteredFighters.Clear();
        }
        else
        {
            filteredFighters = await ViewBaseFightersUseCase.ExecuteAsync(searchQuery);
        }

        StateHasChanged();
    }

    private async Task SelectFighter(int fighterId)
    {
        searchQuery = string.Empty;
        filteredFighters.Clear();

        await OnFighterSelected.InvokeAsync(fighterId);

        SelectedFighter = await ViewFighterByIdUseCase.ExecuteAsync(fighterId);

        StateHasChanged();
    }

    private void ComputePlaceholder()
    {
        if (SelectedFighter != null)
        {
            Placeholder = SelectedFighter.GetFullName();
        }
        else
        {
            Placeholder = "Search Fighter ...";
        }
    }
}
