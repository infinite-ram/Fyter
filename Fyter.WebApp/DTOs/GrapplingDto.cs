using System.ComponentModel.DataAnnotations;

namespace Fyter.WebApp.DTOs;

public class GrapplingDto
{
    [ValidateComplexType]
    public StatDto TakeDowns { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto TopControl { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto BottomControl { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto SubmissionOffense { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto SubmissionDefense { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto GroundStriking { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto ClinchStriking { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto ClinchControl { get; set; } = new StatDto();
}
