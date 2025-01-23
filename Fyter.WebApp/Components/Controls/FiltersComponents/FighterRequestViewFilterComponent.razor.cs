using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterRequestModel;
using Fyter.UseCases.PluginInterfaces;
using Fyter.WebApp.Extensions;
using Fyter.WebApp.Services.Interfaces;

namespace Fyter.WebApp.Components.Controls.FiltersComponents;

public partial class FighterRequestViewFilterComponent : ComponentBase
{
    [Inject]
    public required IFighterStatusFilterRepository FighterStatusFilterRepository { get; set; }

    [Inject]
    public required IAuthService AuthService { get; set; }

    public FighterRequestViewEnum? selectedView { get; set; }

    [Parameter]
    public EventCallback RefreshGrid { get; set; }

    private string? _currentUserId;

    protected override async Task OnInitializedAsync()
    {
        _currentUserId = await AuthService.GetUserIdAsync();
    }

    private string GetButtonLabel()
    {
        return selectedView.HasValue ? selectedView.Value.GetDisplayName() : "View Filter";
    }

    private async Task Reset()
    {
        selectedView = null;
        await FighterStatusFilterRepository.ResetQuery();
        await OnRefreshGrid();
    }

    private async Task OnSelectViewStatus(FighterRequestViewEnum? view)
    {
        selectedView = view;

        if (!string.IsNullOrWhiteSpace(_currentUserId))
            await FighterStatusFilterRepository.SetCurrentUser(_currentUserId);

        await FighterStatusFilterRepository.SetFighterRequestViewFilter(selectedView);
        await OnRefreshGrid();
    }

    private async Task OnRefreshGrid()
    {
        await RefreshGrid.InvokeAsync();
    }
}
