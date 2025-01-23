using System.ComponentModel.DataAnnotations;
using Fyter.CoreBusiness.FighterModel;
using Fyter.CoreBusiness.Validations;

namespace Fyter.WebApp.DTOs;

public class FighterDto
{
    public int FighterId { get; set; }

    private string? _firstName = string.Empty;

    [Required]
    public string? FirstName
    {
        get => _firstName;
        set => _firstName = value?.Trim();
    }

    private string? _lastName = string.Empty;

    [Required]
    public string? LastName
    {
        get => _lastName;
        set => _lastName = value?.Trim();
    }

    [Required]
    [HalfStepRange(1.0, 5.0, ErrorMessage = "Value must be between 1 and 5 in 0.5 increments.")]
    public double FighterStars { get; set; }

    [Required]
    [HalfStepRange(1.0, 5.0)]
    public double FighterStandUpStars { get; set; }

    [Required]
    [HalfStepRange(1.0, 5.0)]
    public double FighterGrapplingStars { get; set; }

    [Required]
    [HalfStepRange(1.0, 5.0)]
    public double FighterHealthStars { get; set; }

    [Required]
    public string EgoName { get; set; } = string.Empty;

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    public FighterStyleEnum FighterStyle { get; set; }

    [ValidateComplexType]
    public StandUpDto StandUp { get; set; } = new StandUpDto();

    [ValidateComplexType]
    public GrapplingDto Grappling { get; set; } = new GrapplingDto();

    [ValidateComplexType]
    public HealthDto Health { get; set; } = new HealthDto();

    [ValidateComplexType]
    public FighterInfoDto FighterInfo { get; set; } = new FighterInfoDto();

    [ValidateComplexType]
    public List<PerksEnum> Perks { get; set; } = new List<PerksEnum>();

    public List<TopMovesDto> TopMoves { get; set; } = new List<TopMovesDto>();

    [Required]
    public DivisionEnum? Division { get; set; }

    [Required]
    public bool IsAlterEgo { get; set; } = false;
    public List<FighterDto>? AlterEgos { get; set; } = new List<FighterDto>();

    public string? AddedByUserId { get; set; }
    public string? LastUpdatedByUserId { get; set; }

    public int? BaseFighterId { get; set; }
    public FighterDto? BaseFighter { get; set; }

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }
}
