using System.ComponentModel.DataAnnotations;
using Fyter.CoreBusiness.FighterModel;

namespace Fyter.WebApp.DTOs;

public class FighterInfoDto
{
    public string? NickName { get; set; }

    [Required]
    [Range(1, 100)]
    public int Age { get; set; }

    [Required]
    public FighterHeightDto Height { get; set; } = new FighterHeightDto();

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
