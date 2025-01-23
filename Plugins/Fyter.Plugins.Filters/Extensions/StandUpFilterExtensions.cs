using Fyter.CoreBusiness.FighterModel;
using Fyter.CoreBusiness.Filters;

namespace Fyter.Plugins.Filters.Extensions;

public static class StandUpFilterExtensions
{
    public static IQueryable<Fighter> ApplyStandUpFilters(
        this IQueryable<Fighter> query,
        FighterStandUpFilter filter
    )
    {
        if (filter.MinPunchSpeed.HasValue)
            query = query.Where(f => f.StandUp.PunchSpeed.Value >= filter.MinPunchSpeed.Value);
        if (filter.MaxPunchSpeed.HasValue)
            query = query.Where(f => f.StandUp.PunchSpeed.Value <= filter.MaxPunchSpeed.Value);

        if (filter.MinPunchPower.HasValue)
            query = query.Where(f => f.StandUp.PunchPower.Value >= filter.MinPunchPower.Value);
        if (filter.MaxPunchPower.HasValue)
            query = query.Where(f => f.StandUp.PunchPower.Value <= filter.MaxPunchPower.Value);

        if (filter.MinAccuracy.HasValue)
            query = query.Where(f => f.StandUp.Accuracy.Value >= filter.MinAccuracy.Value);
        if (filter.MaxAccuracy.HasValue)
            query = query.Where(f => f.StandUp.Accuracy.Value <= filter.MaxAccuracy.Value);

        if (filter.MinBlocking.HasValue)
            query = query.Where(f => f.StandUp.Blocking.Value >= filter.MinBlocking.Value);
        if (filter.MaxBlocking.HasValue)
            query = query.Where(f => f.StandUp.Blocking.Value <= filter.MaxBlocking.Value);

        if (filter.MinHeadMovement.HasValue)
            query = query.Where(f => f.StandUp.HeadMovement.Value >= filter.MinHeadMovement.Value);
        if (filter.MaxHeadMovement.HasValue)
            query = query.Where(f => f.StandUp.HeadMovement.Value <= filter.MaxHeadMovement.Value);

        if (filter.MinFootWork.HasValue)
            query = query.Where(f => f.StandUp.FootWork.Value >= filter.MinFootWork.Value);
        if (filter.MaxFootWork.HasValue)
            query = query.Where(f => f.StandUp.FootWork.Value <= filter.MaxFootWork.Value);

        if (filter.MinSwitchStance.HasValue)
            query = query.Where(f => f.StandUp.SwitchStance.Value >= filter.MinSwitchStance.Value);
        if (filter.MaxSwitchStance.HasValue)
            query = query.Where(f => f.StandUp.SwitchStance.Value <= filter.MaxSwitchStance.Value);

        if (filter.MinTakedownDefense.HasValue)
            query = query.Where(f =>
                f.StandUp.TakedownDefense.Value >= filter.MinTakedownDefense.Value
            );
        if (filter.MaxTakedownDefense.HasValue)
            query = query.Where(f =>
                f.StandUp.TakedownDefense.Value <= filter.MaxTakedownDefense.Value
            );

        if (filter.MinKickPower.HasValue)
            query = query.Where(f => f.StandUp.KickPower.Value >= filter.MinKickPower.Value);
        if (filter.MaxKickPower.HasValue)
            query = query.Where(f => f.StandUp.KickPower.Value <= filter.MaxKickPower.Value);

        if (filter.MinKickSpeed.HasValue)
            query = query.Where(f => f.StandUp.KickSpeed.Value >= filter.MinKickSpeed.Value);
        if (filter.MaxKickSpeed.HasValue)
            query = query.Where(f => f.StandUp.KickSpeed.Value <= filter.MaxKickSpeed.Value);

        return query;
    }
}
