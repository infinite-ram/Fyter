using Fyter.CoreBusiness.Filters.Attributes;

namespace Fyter.CoreBusiness.Filters;

public class FighterStandUpFilter
{
    [FilterDisplay("Punch Speed", "≥")]
    public int? MinPunchSpeed { get; set; }

    [FilterDisplay("Punch Speed", "≤")]
    public int? MaxPunchSpeed { get; set; }

    [FilterDisplay("Punch Power", "≥")]
    public int? MinPunchPower { get; set; }

    [FilterDisplay("Punch Power", "≤")]
    public int? MaxPunchPower { get; set; }

    [FilterDisplay("Accuracy", "≥")]
    public int? MinAccuracy { get; set; }

    [FilterDisplay("Accuracy", "≤")]
    public int? MaxAccuracy { get; set; }

    [FilterDisplay("Blocking", "≥")]
    public int? MinBlocking { get; set; }

    [FilterDisplay("Blocking", "≤")]
    public int? MaxBlocking { get; set; }

    [FilterDisplay("Head Movement", "≥")]
    public int? MinHeadMovement { get; set; }

    [FilterDisplay("Head Movement", "≤")]
    public int? MaxHeadMovement { get; set; }

    [FilterDisplay("Footwork", "≥")]
    public int? MinFootWork { get; set; }

    [FilterDisplay("Footwork", "≤")]
    public int? MaxFootWork { get; set; }

    [FilterDisplay("Switch Stance", "≥")]
    public int? MinSwitchStance { get; set; }

    [FilterDisplay("Switch Stance", "≤")]
    public int? MaxSwitchStance { get; set; }

    [FilterDisplay("Takedown Defense", "≥")]
    public int? MinTakedownDefense { get; set; }

    [FilterDisplay("Takedown Defense", "≤")]
    public int? MaxTakedownDefense { get; set; }

    [FilterDisplay("Kick Power", "≥")]
    public int? MinKickPower { get; set; }

    [FilterDisplay("Kick Power", "≤")]
    public int? MaxKickPower { get; set; }

    [FilterDisplay("Kick Speed", "≥")]
    public int? MinKickSpeed { get; set; }

    [FilterDisplay("Kick Speed", "≤")]
    public int? MaxKickSpeed { get; set; }

    public bool HasFilters()
    {
        // existing logic or remain unchanged
        return MinPunchSpeed.HasValue
            || MaxPunchSpeed.HasValue
            || MinPunchPower.HasValue
            || MaxPunchPower.HasValue
            || MinAccuracy.HasValue
            || MaxAccuracy.HasValue
            || MinBlocking.HasValue
            || MaxBlocking.HasValue
            || MinHeadMovement.HasValue
            || MaxHeadMovement.HasValue
            || MinFootWork.HasValue
            || MaxFootWork.HasValue
            || MinSwitchStance.HasValue
            || MaxSwitchStance.HasValue
            || MinTakedownDefense.HasValue
            || MaxTakedownDefense.HasValue
            || MinKickPower.HasValue
            || MaxKickPower.HasValue
            || MinKickSpeed.HasValue
            || MaxKickSpeed.HasValue;
    }
}
