using Fyter.CoreBusiness.FighterModel;
using Fyter.CoreBusiness.Filters;

namespace Fyter.UseCases.PluginInterfaces;

public interface IFighterFilterRepository
{
    event Action? OnFilterChanged;

    FighterStyleEnum? SelectedFighterStyle { get; }
    DivisionEnum? SelectedDivision { get; }
    PerksEnum? SelectedPerk { get; }

    FighterStandUpFilter StandUpFilter { get; }
    FighterGrapplingFilter GrapplingUpFilter { get; }
    FighterHealthFilter HealthFilter { get; }
    Dictionary<string, string> ActiveFilterDisplays { get; set; }

    Task SetFightersAsQueryable(IQueryable<Fighter> query);
    Task<IQueryable<Fighter>> GetFightersAsQueryable();

    Task SetStandUpFilter(FighterStandUpFilter? standUpFilters);
    Task SetGrapplingFilter(FighterGrapplingFilter? grapplingFilters);
    Task SetHealthFilter(FighterHealthFilter? healthFilters);

    Task SetFighterStyleFilter(FighterStyleEnum? fighterStyle);
    Task SetFighterDivisionFilter(DivisionEnum? division);
    Task SetFighterPerkFilter(PerksEnum? perk);

    bool IsStandUpFilterApplied();
    bool IsGrapplingFilterApplied();
    bool IsHealthFilterApplied();

    Task ResetStandUpFilters();
    Task ResetGrapplingFilters();
    Task ResetHealthFilters();

    Task ResetQuery();
}
