using System.ComponentModel.DataAnnotations;

namespace Fyter.CoreBusiness.FighterModel;

public class StandUp
{
    [ValidateComplexType]
    public Stat PunchSpeed { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat PunchPower { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat Accuracy { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat Blocking { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat HeadMovement { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat FootWork { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat SwitchStance { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat TakedownDefense { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat KickPower { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat KickSpeed { get; set; } = new Stat();
}
