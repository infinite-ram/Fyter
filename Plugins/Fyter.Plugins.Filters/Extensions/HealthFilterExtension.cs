using Fyter.CoreBusiness.FighterModel;
using Fyter.CoreBusiness.Filters;

namespace Fyter.Plugins.Filters.Extensions;

public static class HealthFilterExtension
{
    public static IQueryable<Fighter> ApplyHealthFilters(
        this IQueryable<Fighter> query,
        FighterHealthFilter filter
    )
    {
        if (filter.MinCardio.HasValue)
            query = query.Where(f => f.Health.Cardio.Value >= filter.MinCardio.Value);
        if (filter.MaxCardio.HasValue)
            query = query.Where(f => f.Health.Cardio.Value <= filter.MaxCardio.Value);

        if (filter.MinChin.HasValue)
            query = query.Where(f => f.Health.Chin.Value >= filter.MinChin.Value);
        if (filter.MaxChin.HasValue)
            query = query.Where(f => f.Health.Chin.Value <= filter.MaxChin.Value);

        if (filter.MinBody.HasValue)
            query = query.Where(f => f.Health.Body.Value >= filter.MinBody.Value);
        if (filter.MaxBody.HasValue)
            query = query.Where(f => f.Health.Body.Value <= filter.MaxBody.Value);

        if (filter.MinLegs.HasValue)
            query = query.Where(f => f.Health.Legs.Value >= filter.MinLegs.Value);
        if (filter.MaxLegs.HasValue)
            query = query.Where(f => f.Health.Legs.Value <= filter.MaxLegs.Value);

        if (filter.MinRecovery.HasValue)
            query = query.Where(f => f.Health.Recovery.Value >= filter.MinRecovery.Value);
        if (filter.MaxRecovery.HasValue)
            query = query.Where(f => f.Health.Recovery.Value <= filter.MaxRecovery.Value);

        if (filter.MinCutResistant.HasValue)
            query = query.Where(f => f.Health.CutResistant.Value >= filter.MinCutResistant.Value);
        if (filter.MaxCutResistant.HasValue)
            query = query.Where(f => f.Health.CutResistant.Value <= filter.MaxCutResistant.Value);

        return query;
    }
}
