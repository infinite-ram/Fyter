using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.Filters;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.WebApp.Components.Controls.FiltersComponents;

public partial class ActiveFiltersDisplay : ComponentBase
{
    [Inject]
    public IFighterFilterRepository FighterFilterRepository { get; set; }

    [Parameter]
    public EventCallback OnFilterRemoved { get; set; }

    protected override void OnInitialized()
    {
        FighterFilterRepository.OnFilterChanged += HandleFilterChanged;
    }

    private void HandleFilterChanged()
    {
        InvokeAsync(StateHasChanged);
    }

    private async Task RemoveFilter(string propertyKey)
    {
        // 1) Check StandUpFilter
        var standUpProp = typeof(FighterStandUpFilter).GetProperty(propertyKey);
        if (standUpProp != null && standUpProp.PropertyType == typeof(int?))
        {
            var standUp = FighterFilterRepository.StandUpFilter;
            standUpProp.SetValue(standUp, null);
            await FighterFilterRepository.SetStandUpFilter(standUp);
            await OnFilterRemoved.InvokeAsync();
            return;
        }

        // 2) Check GrapplingFilter
        var grapplingProp = typeof(FighterGrapplingFilter).GetProperty(propertyKey);
        if (grapplingProp != null && grapplingProp.PropertyType == typeof(int?))
        {
            var grappling = FighterFilterRepository.GrapplingUpFilter;
            grapplingProp.SetValue(grappling, null);
            await FighterFilterRepository.SetGrapplingFilter(grappling);
            await OnFilterRemoved.InvokeAsync();
            return;
        }

        // 3) Check HealthFilter
        var healthProp = typeof(FighterHealthFilter).GetProperty(propertyKey);
        if (healthProp != null && healthProp.PropertyType == typeof(int?))
        {
            var health = FighterFilterRepository.HealthFilter;
            healthProp.SetValue(health, null);
            await FighterFilterRepository.SetHealthFilter(health);
            await OnFilterRemoved.InvokeAsync();
            return;
        }

        // 4) If it’s something else (e.g. FighterStyle, Division, Perk), do your existing code:
        switch (propertyKey)
        {
            case "FighterStyle":
                await FighterFilterRepository.SetFighterStyleFilter(null);
                break;
            case "Division":
                await FighterFilterRepository.SetFighterDivisionFilter(null);
                break;
            case "Perk":
                await FighterFilterRepository.SetFighterPerkFilter(null);
                break;
            // etc
        }

        await OnFilterRemoved.InvokeAsync();
    }

    public void Dispose()
    {
        FighterFilterRepository.OnFilterChanged -= HandleFilterChanged;
    }
}
