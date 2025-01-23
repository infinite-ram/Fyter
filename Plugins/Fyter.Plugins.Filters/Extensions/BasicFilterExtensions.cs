using Fyter.CoreBusiness.FighterModel;

namespace Fyter.Plugins.Filters.Extensions;

public static class BasicFilterExtensions
{
    public static IQueryable<Fighter> ApplyBasicFilters(
        this IQueryable<Fighter> query,
        FighterStyleEnum? fighterStyleFilter,
        DivisionEnum? divisionFilter,
        PerksEnum? perkFilter
    )
    {
        if (fighterStyleFilter.HasValue)
            query = query.Where(f => f.FighterStyle == fighterStyleFilter.Value);

        if (divisionFilter.HasValue)
            query = query.Where(f => f.Division == divisionFilter.Value);

        if (perkFilter.HasValue)
            query = query.Where(f => f.Perks.Contains(perkFilter.Value));

        return query;
    }
}
