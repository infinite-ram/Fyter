using System.ComponentModel.DataAnnotations;
using Fyter.CoreBusiness.Validations;

namespace Fyter.CoreBusiness.FighterModel;

public class Stat
{
    [Required(ErrorMessage = "Value is required.")]
    [Range(1, 100, ErrorMessage = "Value must be between 1 and 100.")]
    public int Value { get; set; }

    [Required(ErrorMessage = "Stars are required.")]
    [HalfStepRange(1.0, 5.0, ErrorMessage = "Value must be between 1 and 5 in 0.5 increments.")]
    public double Stars { get; set; }
}
