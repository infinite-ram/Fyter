using Fyter.CoreBusiness.FighterModel;
using Fyter.CoreBusiness.Filters;

namespace Fyter.Plugins.Filters.Extensions;

public static class GrapplingFilterExtension
{
    public static IQueryable<Fighter> ApplyGrapplingFilters(
        this IQueryable<Fighter> query,
        FighterGrapplingFilter filter
    )
    {
        if (filter.MinTakeDown.HasValue)
            query = query.Where(f => f.Grappling.TakeDowns.Value >= filter.MinTakeDown.Value);
        if (filter.MaxTakeDown.HasValue)
            query = query.Where(f => f.Grappling.TakeDowns.Value <= filter.MaxTakeDown.Value);

        if (filter.MinTopControl.HasValue)
            query = query.Where(f => f.Grappling.TopControl.Value >= filter.MinTopControl.Value);
        if (filter.MaxTopControl.HasValue)
            query = query.Where(f => f.Grappling.TopControl.Value <= filter.MaxTopControl.Value);

        if (filter.MinBottomControl.HasValue)
            query = query.Where(f =>
                f.Grappling.BottomControl.Value >= filter.MinBottomControl.Value
            );
        if (filter.MaxBottomControl.HasValue)
            query = query.Where(f =>
                f.Grappling.BottomControl.Value >= filter.MaxBottomControl.Value
            );

        if (filter.MinSubmissionOffense.HasValue)
            query = query.Where(f =>
                f.Grappling.SubmissionOffense.Value >= filter.MinSubmissionOffense.Value
            );
        if (filter.MaxSubmissionOffense.HasValue)
            query = query.Where(f =>
                f.Grappling.SubmissionOffense.Value <= filter.MaxSubmissionOffense.Value
            );

        if (filter.MinSubmissionDefense.HasValue)
            query = query.Where(f =>
                f.Grappling.SubmissionDefense.Value >= filter.MinSubmissionDefense.Value
            );
        if (filter.MaxSubmissionDefense.HasValue)
            query = query.Where(f =>
                f.Grappling.SubmissionDefense.Value <= filter.MaxSubmissionDefense.Value
            );

        if (filter.MinGroundStriking.HasValue)
            query = query.Where(f =>
                f.Grappling.GroundStriking.Value >= filter.MinGroundStriking.Value
            );
        if (filter.MaxGroundStriking.HasValue)
            query = query.Where(f =>
                f.Grappling.GroundStriking.Value <= filter.MaxGroundStriking.Value
            );

        if (filter.MinClinchStriking.HasValue)
            query = query.Where(f =>
                f.Grappling.ClinchStriking.Value >= filter.MinClinchStriking.Value
            );
        if (filter.MaxClinchStriking.HasValue)
            query = query.Where(f =>
                f.Grappling.ClinchStriking.Value <= filter.MaxClinchStriking.Value
            );

        if (filter.MinClinchControl.HasValue)
            query = query.Where(f =>
                f.Grappling.ClinchControl.Value >= filter.MinClinchControl.Value
            );
        if (filter.MaxClinchControl.HasValue)
            query = query.Where(f =>
                f.Grappling.ClinchControl.Value <= filter.MaxClinchControl.Value
            );

        return query;
    }
}
