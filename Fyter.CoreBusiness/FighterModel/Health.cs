using System.ComponentModel.DataAnnotations;

namespace Fyter.CoreBusiness.FighterModel;

public class Health
{
    [ValidateComplexType]
    public Stat Cardio { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat Chin { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat Body { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat Legs { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat Recovery { get; set; } = new Stat();

    [ValidateComplexType]
    public Stat CutResistant { get; set; } = new Stat();
}
