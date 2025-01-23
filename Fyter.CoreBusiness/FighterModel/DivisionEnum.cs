using System.ComponentModel.DataAnnotations;

namespace Fyter.CoreBusiness.FighterModel;

public enum DivisionEnum
{
    [Display(Name = "Straw Weight (W)")]
    WomenStrawWeight,

    [Display(Name = "Fly Weight (W)")]
    WomenFlyWeight,

    [Display(Name = "Bantam Weight (W)")]
    WomenBantamWeight,

    [Display(Name = "Fly Weight")]
    FlyWeight,

    [Display(Name = "Bantam Weight")]
    BantamWeight,

    [Display(Name = "Feather Weight")]
    FeatherWeight,

    [Display(Name = "Light Weight")]
    LightWeight,

    [Display(Name = "Welter Weight")]
    WelterWeight,

    [Display(Name = "Middle Weight")]
    MiddleWeight,

    [Display(Name = "Light Heavy Weight")]
    LightHeavyWeight,

    [Display(Name = "Heavy Weight")]
    HeavyWeight,
}
