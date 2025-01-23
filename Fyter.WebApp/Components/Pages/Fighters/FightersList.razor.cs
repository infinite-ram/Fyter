using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;
using Fyter.UseCases.Filters.Interfaces;

namespace Fyter.WebApp.Components.Pages.Fighters;

public partial class FightersList : ComponentBase
{
    [Inject]
    public required IViewFightersByNameUseCase ViewFightersByNameUseCase { get; set; }

    [Inject]
    public required ISetFightersAsQueryableUseCase SetFightersAsQueryableUseCase { get; set; }

    [Inject]
    public required IGetFightersAsQueryableUseCase GetFightersAsQueryableUseCase { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    private QuickGrid<Fighter>? grid;
    private PaginationState pagination = new PaginationState { ItemsPerPage = 23 };

    private string nameFilter = string.Empty;
    private int totalResults = 0;

    private GridItemsProvider<Fighter> itemsProvider;
    private List<Fighter> fighters = new List<Fighter>();
    private IQueryable<Fighter>? query;

    protected override async Task OnInitializedAsync()
    {
        fighters = await ViewFightersByNameUseCase.ExecuteAsync(string.Empty);
        fighters = fighters.Where(f => f.IsAlterEgo == false).ToList();

        query = fighters.AsQueryable();

        itemsProvider = async request =>
        {
            if (!string.IsNullOrWhiteSpace(nameFilter))
                query = query.Where(f =>
                    f.GetFullName().Contains(nameFilter, StringComparison.OrdinalIgnoreCase)
                );

            // Set the query
            await SetFightersAsQueryableUseCase.ExecuteAsync(query);

            // Get query. Includes applied filters.
            query = await GetFightersAsQueryableUseCase.ExecuteAsync();

            // Apply sorting
            query = request.ApplySorting(query);

            var totalCount = query.Count();
            if (totalResults != totalCount && !request.CancellationToken.IsCancellationRequested)
            {
                totalResults = totalCount;
                await InvokeAsync(StateHasChanged);
            }

            // Upply pagination
            var itemsQuery = query.Skip(request.StartIndex);
            if (request.Count.HasValue)
            {
                itemsQuery = itemsQuery.Take(request.Count.Value);
            }

            var items = itemsQuery.ToList();

            return GridItemsProviderResult.From(items, totalCount);
        };
    }

    private void ViewFighter(Fighter fighter)
    {
        NavigationManager.NavigateTo($"/fighter/{fighter.FighterId}");
    }

    private void HandleOnSearchInput(string searchedTerm)
    {
        nameFilter = searchedTerm;
        grid?.RefreshDataAsync();
    }

    private async Task RefreshGrid()
    {
        query = await GetFightersAsQueryableUseCase.ExecuteAsync();
        await grid?.RefreshDataAsync();

        StateHasChanged();
    }
}
