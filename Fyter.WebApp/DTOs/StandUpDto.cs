using System.ComponentModel.DataAnnotations;

namespace Fyter.WebApp.DTOs;

public class StandUpDto
{
    [ValidateComplexType]
    public StatDto PunchSpeed { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto PunchPower { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto Accuracy { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto Blocking { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto HeadMovement { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto FootWork { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto SwitchStance { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto TakedownDefense { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto KickPower { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto KickSpeed { get; set; } = new StatDto();
}
