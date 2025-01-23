using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.Filters;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.WebApp.Components.Controls.FiltersComponents;

public partial class FightersStandUpFilterComponent : ComponentBase
{
    [Inject]
    public required IFighterFilterRepository FighterFilterRepository { get; set; }

    [Parameter]
    public required FighterStandUpFilter StandUpFilter { get; set; }

    [Parameter]
    public EventCallback RefreshGrid { get; set; }

    private async Task HandleFilter()
    {
        await FighterFilterRepository.SetStandUpFilter(StandUpFilter);
        await RefreshGrid.InvokeAsync();
    }

    private Task ResetPunchSpeed() =>
        ResetFilter(() => StandUpFilter.MinPunchSpeed, () => StandUpFilter.MaxPunchSpeed);

    private Task ResetPunchPower() =>
        ResetFilter(() => StandUpFilter.MinPunchPower, () => StandUpFilter.MaxPunchPower);

    private Task ResetAccuracy() =>
        ResetFilter(() => StandUpFilter.MinAccuracy, () => StandUpFilter.MaxAccuracy);

    private Task ResetBlocking() =>
        ResetFilter(() => StandUpFilter.MinBlocking, () => StandUpFilter.MaxBlocking);

    private Task ResetHeadMovement() =>
        ResetFilter(() => StandUpFilter.MinHeadMovement, () => StandUpFilter.MaxHeadMovement);

    private Task ResetFootWork() =>
        ResetFilter(() => StandUpFilter.MinFootWork, () => StandUpFilter.MaxFootWork);

    private Task ResetSwitchStance() =>
        ResetFilter(() => StandUpFilter.MinSwitchStance, () => StandUpFilter.MaxSwitchStance);

    private Task ResetTakedownDefense() =>
        ResetFilter(() => StandUpFilter.MinTakedownDefense, () => StandUpFilter.MaxTakedownDefense);

    private Task ResetKickPower() =>
        ResetFilter(() => StandUpFilter.MinKickPower, () => StandUpFilter.MaxKickPower);

    private Task ResetKickSpeed() =>
        ResetFilter(() => StandUpFilter.MinKickSpeed, () => StandUpFilter.MaxKickSpeed);

    private async Task ResetFilter(
        Expression<Func<int?>> StandUpFilterMinPropertySelector,
        Expression<Func<int?>> StandUpFilterMaxPropertySelector
    )
    {
        // Reset the corresponding Min property in FighterStandUpFilter
        var filterMinProperty = (StandUpFilterMinPropertySelector.Body as MemberExpression)
            ?.Member
            .Name;
        if (filterMinProperty != null)
        {
            typeof(FighterStandUpFilter)
                .GetProperty(filterMinProperty)
                ?.SetValue(StandUpFilter, null);
        }

        // Reset the corresponding Max property in FighterStandUpFilter
        var filterMaxProperty = (StandUpFilterMaxPropertySelector.Body as MemberExpression)
            ?.Member
            .Name;
        if (filterMaxProperty != null)
        {
            typeof(FighterStandUpFilter)
                .GetProperty(filterMaxProperty)
                ?.SetValue(StandUpFilter, null);
        }

        // Apply the filter changes
        await FighterFilterRepository.SetStandUpFilter(StandUpFilter);
        await RefreshGrid.InvokeAsync();
    }

    private async Task Reset()
    {
        StandUpFilter = new();
        await FighterFilterRepository.SetStandUpFilter(StandUpFilter);

        await RefreshGrid.InvokeAsync();
    }
}
