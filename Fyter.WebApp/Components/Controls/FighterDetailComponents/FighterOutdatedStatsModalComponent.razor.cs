using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;
using Fyter.WebApp.DTOs;
using Fyter.WebApp.Services.Interfaces;

namespace Fyter.WebApp.Components.Controls.FighterDetailComponents;

public partial class FighterOutdatedStatsModalComponent : ComponentBase
{
    [Inject]
    public IAuthService AuthService { get; set; } = default!;

    [Inject]
    public ISetFighterStatOutdatedUsecase SetFighterStatOutdatedUsecase { get; set; } = default!;

    [Inject]
    public IFighterPropertyPathService FighterPropertyPathService { get; set; } = default!;

    [Parameter]
    public required Fighter Fighter { get; set; }

    [Parameter]
    public EventCallback OnRefresh { get; set; }

    private bool isOutdatedModalVisible;
    private OutdatedStatsDto outdatedStatsViewModel = new();
    private List<string> allProps = new();

    private void ShowOutdatedModal()
    {
        if (Fighter != null)
        {
            allProps = FighterPropertyPathService.GetAllPropertyPaths(Fighter);
            outdatedStatsViewModel.Initialize(Fighter, allProps);
        }

        isOutdatedModalVisible = true;
    }

    private async Task SaveOutdatedStatsAsync()
    {
        if (Fighter != null)
        {
            var userId = await AuthService.GetUserIdAsync();

            foreach (var kvp in outdatedStatsViewModel.SelectedOutdatedStats)
            {
                Fighter.OutdatedStats[kvp.Key] = new OutdatedStatus
                {
                    IsOutdated = kvp.Value.IsOutdated,
                };
            }

            Fighter.IsOutdated = Fighter.OutdatedStats.Any(kvp => kvp.Value.IsOutdated);

            await SetFighterStatOutdatedUsecase.ExecuteAsync(Fighter, userId);

            HideOutdatedModal();

            await OnRefresh.InvokeAsync();
        }
    }

    private void HideOutdatedModal()
    {
        isOutdatedModalVisible = false;
        outdatedStatsViewModel = new OutdatedStatsDto();
    }

    private void SelectAll()
    {
        outdatedStatsViewModel.SelectAll();
    }

    private void DeselectAll()
    {
        outdatedStatsViewModel.DeselectAll();
    }
}
