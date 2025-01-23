using System.ComponentModel.DataAnnotations;

namespace Fyter.CoreBusiness.FighterModel;

public class Grappling
{
    [ValidateComplexType]
    public Stat TakeDowns { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat TopControl { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat BottomControl { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat SubmissionOffense { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat SubmissionDefense { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat GroundStriking { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat ClinchStriking { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat ClinchControl { get; set; } = new Stat();
}
