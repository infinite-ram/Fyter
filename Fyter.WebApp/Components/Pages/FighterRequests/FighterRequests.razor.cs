using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Fyter.CoreBusiness.FighterModel;
using Fyter.CoreBusiness.FighterRequestModel;
using Fyter.CoreBusiness.FighterRequestModel.ViewModel;
using Fyter.UseCases.FighterRequests.Interfaces;
using Fyter.UseCases.Fighters.Interfaces;
using Fyter.UseCases.PluginInterfaces;
using Fyter.WebApp.Services.Interfaces;

namespace Fyter.WebApp.Components.Pages.FighterRequests;

public partial class FighterRequests : ComponentBase
{
    [Inject]
    public required IAuthService AuthService { get; set; }

    [Inject]
    public required IViewOutdatedFightersByNameUseCase ViewOutdatedFightersByNameUseCase { get; set; }

    [Inject]
    public required IViewFighterRequestsByNameUseCase ViewFighterRequestsByNameUseCase { get; set; }

    [Inject]
    public required IViewFighterRequestByIdUseCase ViewFighterRequestByIdUseCase { get; set; }

    [Inject]
    public required IFighterStatusFilterRepository FighterStatusFilterRepository { get; set; }

    [Inject]
    public required IDeleteFighterRequestUseCase DeleteFighterRequestUseCase { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    private List<Fighter> fighterUpdateRequests = new List<Fighter>();
    private List<FighterRequest> fighterAddRequests = new();
    private List<FighterRequestViewModel> fightersView = new();

    private string nameFilter = string.Empty;
    private int totalResults = 0;
    private PaginationState pagination = new PaginationState { ItemsPerPage = 8 };

    private QuickGrid<FighterRequestViewModel>? grid;
    private IQueryable<FighterRequestViewModel> query;
    private GridItemsProvider<FighterRequestViewModel>? itemsProvider;

    private FighterRequestViewModel? selectedViewModel;
    private FighterRequest? selectedFighterRequest = new();

    private string currentUserId;

    protected override async Task OnInitializedAsync()
    {
        currentUserId = await AuthService.GetUserIdAsync();
        fighterUpdateRequests = await ViewOutdatedFightersByNameUseCase.ExecuteAsync(nameFilter);
        fighterAddRequests = await ViewFighterRequestsByNameUseCase.ExecuteAsync(nameFilter);
        await CombineFighterLists();

        query = fightersView.AsQueryable();

        itemsProvider = async request =>
        {
            if (!string.IsNullOrWhiteSpace(nameFilter))
                query = query.Where(f =>
                    f.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase)
                );

            // Set the query
            await FighterStatusFilterRepository.SetFightersAsQueryable(query);

            // Get query. Includes applied filters.
            query = await FighterStatusFilterRepository.GetFightersAsQueryable();

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

    private void NavigateToFighter(FighterRequestViewModel viewModel)
    {
        NavigationManager.NavigateTo($"addfighter/{viewModel.FighterRequestId}");
    }

    private void NavigateToEditFighter(FighterRequestViewModel viewModel)
    {
        NavigationManager.NavigateTo($"/editfighter/{viewModel.OutedatedFighterId}");
    }

    private void EditFighterRequest(FighterRequestViewModel viewModel)
    {
        NavigationManager.NavigateTo($"editfighterrequest/{viewModel.FighterRequestId}");
    }

    private async Task CombineFighterLists()
    {
        fightersView = await FighterStatusFilterRepository.CreateFightersStatusList();
    }

    private void HandleOnSearchInput(string searchedTerm)
    {
        nameFilter = searchedTerm;
        grid?.RefreshDataAsync();
    }

    private async Task DeleteFighterRequest(FighterRequest selectedFighterRequest)
    {
        if (await AuthService.IsUserAuthenticatedAsync())
        {
            await DeleteFighterRequestUseCase.ExecuteAsync(selectedFighterRequest);
            NavigationManager.NavigateTo("/fighterrequests", forceLoad: true);
        }
    }

    private async Task SelectFighterRequest(FighterRequestViewModel viewModel)
    {
        var fighterRequestId = viewModel.FighterRequestId;
        selectedFighterRequest = await ViewFighterRequestByIdUseCase.ExecuteAsync(fighterRequestId);
        StateHasChanged();
    }

    private async Task RefreshGrid()
    {
        query = await FighterStatusFilterRepository.GetFightersAsQueryable();
        grid?.RefreshDataAsync();
    }
}
