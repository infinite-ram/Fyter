using System.ComponentModel.DataAnnotations;

namespace Fyter.CoreBusiness.FighterModel;

public enum StanceEnum
{
    [Display(Name = "Orthodox")]
    Orthodox,

    [Display(Name = "Southpaw")]
    Southpaw,

    [Display(Name = "Switch")]
    Switch,
}
