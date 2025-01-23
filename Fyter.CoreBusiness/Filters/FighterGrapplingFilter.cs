using Fyter.CoreBusiness.Filters.Attributes;

namespace Fyter.CoreBusiness.Filters;

public class FighterGrapplingFilter
{
    [FilterDisplay("Takedown", "≥")]
    public int? MinTakeDown { get; set; }

    [FilterDisplay("Takedown", "≤")]
    public int? MaxTakeDown { get; set; }

    [FilterDisplay("Top Control", "≥")]
    public int? MinTopControl { get; set; }

    [FilterDisplay("Top Control", "≤")]
    public int? MaxTopControl { get; set; }

    [FilterDisplay("Bottom Control", "≥")]
    public int? MinBottomControl { get; set; }

    [FilterDisplay("Bottom Control", "≤")]
    public int? MaxBottomControl { get; set; }

    [FilterDisplay("Submission Offense", "≥")]
    public int? MinSubmissionOffense { get; set; }

    [FilterDisplay("Submission Offense", "≤")]
    public int? MaxSubmissionOffense { get; set; }

    [FilterDisplay("Submission Defense", "≥")]
    public int? MinSubmissionDefense { get; set; }

    [FilterDisplay("Submission Defense", "≤")]
    public int? MaxSubmissionDefense { get; set; }

    [FilterDisplay("Ground Striking", "≥")]
    public int? MinGroundStriking { get; set; }

    [FilterDisplay("Ground Striking", "≤")]
    public int? MaxGroundStriking { get; set; }

    [FilterDisplay("Clinch Striking", "≥")]
    public int? MinClinchStriking { get; set; }

    [FilterDisplay("Clinch Striking", "≤")]
    public int? MaxClinchStriking { get; set; }

    [FilterDisplay("Clinch Control", "≥")]
    public int? MinClinchControl { get; set; }

    [FilterDisplay("Clinch Control", "≤")]
    public int? MaxClinchControl { get; set; }

    public bool HasFilters()
    {
        return MinTakeDown.HasValue
            || MaxTakeDown.HasValue
            || MinTopControl.HasValue
            || MaxTopControl.HasValue
            || MinBottomControl.HasValue
            || MaxBottomControl.HasValue
            || MinSubmissionOffense.HasValue
            || MaxSubmissionOffense.HasValue
            || MinSubmissionDefense.HasValue
            || MaxSubmissionDefense.HasValue
            || MinGroundStriking.HasValue
            || MaxGroundStriking.HasValue
            || MinClinchControl.HasValue
            || MaxClinchControl.HasValue
            || MinClinchStriking.HasValue
            || MaxClinchStriking.HasValue;
    }
}
