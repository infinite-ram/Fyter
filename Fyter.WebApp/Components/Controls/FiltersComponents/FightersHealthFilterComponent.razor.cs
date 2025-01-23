using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.Filters;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.WebApp.Components.Controls.FiltersComponents;

public partial class FightersHealthFilterComponent : ComponentBase
{
    [Inject]
    public required IFighterFilterRepository FighterFilterRepository { get; set; }

    [Parameter]
    public required FighterHealthFilter HealthFilter { get; set; }

    [Parameter]
    public EventCallback RefreshGrid { get; set; }

    private async Task HandleFilter()
    {
        await FighterFilterRepository.SetHealthFilter(HealthFilter);
        await RefreshGrid.InvokeAsync();
    }

    private Task ResetCardio() =>
        ResetFilter(() => HealthFilter.MinCardio, () => HealthFilter.MaxCardio);

    private Task ResetChin() => ResetFilter(() => HealthFilter.MinChin, () => HealthFilter.MaxChin);

    private Task ResetBody() => ResetFilter(() => HealthFilter.MinBody, () => HealthFilter.MaxBody);

    private Task ResetLegs() => ResetFilter(() => HealthFilter.MinLegs, () => HealthFilter.MaxLegs);

    private Task ResetRecovery() =>
        ResetFilter(() => HealthFilter.MinRecovery, () => HealthFilter.MaxRecovery);

    private Task ResetCutResistant() =>
        ResetFilter(() => HealthFilter.MinCutResistant, () => HealthFilter.MaxCutResistant);

    private async Task ResetFilter(
        Expression<Func<int?>> HealthFilterMinPropertySelector,
        Expression<Func<int?>> HealthFilterMaxPropertySelector
    )
    {
        // Reset the corresponding Min property in FighterStandUpFilter
        var filterMinProperty = (HealthFilterMinPropertySelector.Body as MemberExpression)
            ?.Member
            .Name;
        if (filterMinProperty != null)
        {
            typeof(FighterHealthFilter)
                .GetProperty(filterMinProperty)
                ?.SetValue(HealthFilter, null);
        }

        // Reset the corresponding Max property in FighterStandUpFilter
        var filterMaxProperty = (HealthFilterMaxPropertySelector.Body as MemberExpression)
            ?.Member
            .Name;
        if (filterMaxProperty != null)
        {
            typeof(FighterHealthFilter)
                .GetProperty(filterMaxProperty)
                ?.SetValue(HealthFilter, null);
        }

        // Apply the filter changes
        await FighterFilterRepository.SetHealthFilter(HealthFilter);
        await RefreshGrid.InvokeAsync();
    }

    private async Task Reset()
    {
        HealthFilter = new FighterHealthFilter();
        await FighterFilterRepository.SetHealthFilter(HealthFilter);

        await RefreshGrid.InvokeAsync();
    }
}
