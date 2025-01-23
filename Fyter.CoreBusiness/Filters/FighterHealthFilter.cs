using Fyter.CoreBusiness.Filters.Attributes;

namespace Fyter.CoreBusiness.Filters;

public class FighterHealthFilter
{
    [FilterDisplay("Cardio", "≥")]
    public int? MinCardio { get; set; }

    [FilterDisplay("Cardio", "≤")]
    public int? MaxCardio { get; set; }

    [FilterDisplay("Chin", "≥")]
    public int? MinChin { get; set; }

    [FilterDisplay("Chin", "≤")]
    public int? MaxChin { get; set; }

    [FilterDisplay("Body", "≥")]
    public int? MinBody { get; set; }

    [FilterDisplay("Body", "≤")]
    public int? MaxBody { get; set; }

    [FilterDisplay("Legs", "≥")]
    public int? MinLegs { get; set; }

    [FilterDisplay("Legs", "≤")]
    public int? MaxLegs { get; set; }

    [FilterDisplay("Recovery", "≥")]
    public int? MinRecovery { get; set; }

    [FilterDisplay("Recovery", "≤")]
    public int? MaxRecovery { get; set; }

    [FilterDisplay("Cut Resistant", "≥")]
    public int? MinCutResistant { get; set; }

    [FilterDisplay("Cut Resistant", "≤")]
    public int? MaxCutResistant { get; set; }

    public bool HasValue()
    {
        return MinCardio.HasValue
            || MaxCardio.HasValue
            || MinChin.HasValue
            || MaxChin.HasValue
            || MinBody.HasValue
            || MaxBody.HasValue
            || MinLegs.HasValue
            || MaxLegs.HasValue
            || MinRecovery.HasValue
            || MaxRecovery.HasValue
            || MinCutResistant.HasValue
            || MaxCutResistant.HasValue;
    }
}
