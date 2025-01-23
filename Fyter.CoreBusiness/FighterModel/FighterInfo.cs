using System.ComponentModel.DataAnnotations;

namespace Fyter.CoreBusiness.FighterModel;

public class FighterInfo
{
    public string? NickName { get; set; }

    [Required]
    [Range(1, 100)]
    public int Age { get; set; }

    [Required]
    public FighterHeight Height { get; set; } = new FighterHeight();

    [Required]
    [Range(1, 400)]
    public int Weight { get; set; }

    [Required]
    [Range(1, 100)]
    public int Reach { get; set; }

    [Required]
    public StanceEnum Stance { get; set; } = StanceEnum.Orthodox;

    [Required]
    public string HomeTown { get; set; } = string.Empty;

    [Required]
    public string FightingOutOf { get; set; } = string.Empty;
}

public class FighterHeight
{
    [Required]
    [Range(1, 9)]
    public int Feet { get; set; }

    [Required]
    [Range(1, 11)]
    public int Inches { get; set; }
}
