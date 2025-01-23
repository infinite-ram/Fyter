using System.ComponentModel.DataAnnotations;
using Fyter.CoreBusiness.Validations;

namespace Fyter.CoreBusiness.FighterModel;

public class TopMoves
{
    public int Id { get; set; }

    [Required]
    public string MoveName { get; set; } = string.Empty;

    [Required]
    [HalfStepRange(1.0, 5.0, ErrorMessage = "Value must be between 1 and 5 in 0.5 increments.")]
    public double Stars { get; set; }
}
