using System.ComponentModel.DataAnnotations;

namespace Fyter.CoreBusiness.FighterModel;

public enum FighterStyleEnum
{
    [Display(Name = "Boxer")]
    Boxer,

    [Display(Name = "Karate")]
    Karate,

    [Display(Name = "Kick Boxer")]
    KickBoxer,

    [Display(Name = "Wrestler")]
    Wrestler,

    [Display(Name = "Jiu Jitsu")]
    JiuJitsu,

    [Display(Name = "Taekwondo")]
    Taekwondo,

    [Display(Name = "Free Styler")]
    FreeStyler,

    [Display(Name = "Sambo")]
    Sambo,

    [Display(Name = "Look See Do")]
    LookSeeDo,

    [Display(Name = "MMA")]
    MMA,

    [Display(Name = "Muay Thai")]
    MuayThai,

    [Display(Name = "Greco-Roman Wrestler")]
    GrecoRomanWrestler,

    [Display(Name = "Brazilian Jiu-Jitsu")]
    BrazilianJiuJitsu,

    [Display(Name = "Judo")]
    Judo,

    [Display(Name = "Capoeira")]
    Capoeira,

    [Display(Name = "Catch Wrestling")]
    CatchWrestling,

    [Display(Name = "Kung Fu")]
    KungFu,

    [Display(Name = "Shootfighting")]
    Shootfighting,

    [Display(Name = "Savate")]
    Savate,

    [Display(Name = "Lethwei")]
    Lethwei,

    [Display(Name = "Pancrase")]
    Pancrase,

    [Display(Name = "Vale Tudo")]
    ValeTudo,
}
