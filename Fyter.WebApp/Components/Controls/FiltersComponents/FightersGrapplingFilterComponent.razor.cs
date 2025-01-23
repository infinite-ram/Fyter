using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.Filters;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.WebApp.Components.Controls.FiltersComponents;

public partial class FightersGrapplingFilterComponent : ComponentBase
{
    [Inject]
    public required IFighterFilterRepository FighterFilterRepository { get; set; }

    [Parameter]
    public required FighterGrapplingFilter GrapplingFilter { get; set; }

    [Parameter]
    public EventCallback RefreshGrid { get; set; }

    private async Task HandleFilter()
    {
        await FighterFilterRepository.SetGrapplingFilter(GrapplingFilter);
        await RefreshGrid.InvokeAsync();
    }

    private Task ResetTakeDown() =>
        ResetFilter(() => GrapplingFilter.MinTakeDown, () => GrapplingFilter.MaxTakeDown);

    private Task ResetTopControl() =>
        ResetFilter(() => GrapplingFilter.MinTopControl, () => GrapplingFilter.MaxTopControl);

    private Task ResetBottomControl() =>
        ResetFilter(() => GrapplingFilter.MinBottomControl, () => GrapplingFilter.MaxBottomControl);

    private Task ResetSubOf() =>
        ResetFilter(
            () => GrapplingFilter.MinSubmissionOffense,
            () => GrapplingFilter.MaxSubmissionOffense
        );

    private Task ResetSubDef() =>
        ResetFilter(
            () => GrapplingFilter.MinSubmissionDefense,
            () => GrapplingFilter.MaxSubmissionDefense
        );

    private Task ResetGroundStriking() =>
        ResetFilter(
            () => GrapplingFilter.MinGroundStriking,
            () => GrapplingFilter.MaxGroundStriking
        );

    private Task ResetClinchStriking() =>
        ResetFilter(
            () => GrapplingFilter.MinClinchStriking,
            () => GrapplingFilter.MaxClinchStriking
        );

    private Task ResetClinchControl() =>
        ResetFilter(() => GrapplingFilter.MinClinchControl, () => GrapplingFilter.MaxClinchControl);

    private async Task ResetFilter(
        Expression<Func<int?>> GrapplingFilterMinPropertySelector,
        Expression<Func<int?>> GrapplingFilterMaxPropertySelector
    )
    {
        // Reset the corresponding Min property in FighterStandUpFilter
        var filterMinProperty = (GrapplingFilterMinPropertySelector.Body as MemberExpression)
            ?.Member
            .Name;
        if (filterMinProperty != null)
        {
            typeof(FighterStandUpFilter)
                .GetProperty(filterMinProperty)
                ?.SetValue(GrapplingFilter, null);
        }

        // Reset the corresponding Max property in FighterStandUpFilter
        var filterMaxProperty = (GrapplingFilterMaxPropertySelector.Body as MemberExpression)
            ?.Member
            .Name;
        if (filterMaxProperty != null)
        {
            typeof(FighterStandUpFilter)
                .GetProperty(filterMaxProperty)
                ?.SetValue(GrapplingFilter, null);
        }

        // Apply the filter changes
        await FighterFilterRepository.SetGrapplingFilter(GrapplingFilter);
        await RefreshGrid.InvokeAsync();
    }

    private async Task Reset()
    {
        GrapplingFilter = new FighterGrapplingFilter();
        await FighterFilterRepository.SetGrapplingFilter(GrapplingFilter);

        await RefreshGrid.InvokeAsync();
    }
}
