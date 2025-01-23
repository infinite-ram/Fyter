using System.ComponentModel.DataAnnotations;
using Fyter.CoreBusiness.Validations;

namespace Fyter.CoreBusiness.FighterModel;

public class Fighter
{
    public int FighterId { get; set; }

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [HalfStepRange(1.0, 5.0, ErrorMessage = "Value must be between 1 and 5 in 0.5 increments.")]
    public double FighterStars { get; set; }

    [Required]
    [HalfStepRange(1.0, 5.0, ErrorMessage = "Value must be between 1 and 5 in 0.5 increments.")]
    public double FighterStandUpStars { get; set; }

    [Required]
    [HalfStepRange(1.0, 5.0, ErrorMessage = "Value must be between 1 and 5 in 0.5 increments.")]
    public double FighterGrapplingStars { get; set; }

    [Required]
    [HalfStepRange(1.0, 5.0, ErrorMessage = "Value must be between 1 and 5 in 0.5 increments.")]
    public double FighterHealthStars { get; set; }

    [Required]
    public string EgoName { get; set; } = string.Empty;

    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    public FighterStyleEnum FighterStyle { get; set; }

    [ValidateComplexType]
    public StandUp StandUp { get; set; } = new StandUp();

    [ValidateComplexType]
    public Grappling Grappling { get; set; } = new Grappling();

    [ValidateComplexType]
    public Health Health { get; set; } = new Health();

    [ValidateComplexType]
    public FighterInfo FighterInfo { get; set; } = new FighterInfo();

    [ValidateComplexType]
    public List<PerksEnum> Perks { get; set; } = new List<PerksEnum>();

    public List<TopMoves> TopMoves { get; set; } = new List<TopMoves>();

    [Required]
    public DivisionEnum? Division { get; set; }

    [Required]
    public bool IsAlterEgo { get; set; } = false;

    public List<Fighter>? AlterEgos { get; set; } = new List<Fighter>();

    public int? BaseFighterId { get; set; }
    public Fighter? BaseFighter { get; set; }

    public string? AddedByUserId { get; set; }
    public string? LastUpdatedByUserId { get; set; }
    public string? OutdatedByUserId { get; set; }

    public bool IsOutdated { get; set; } = false;
    public Dictionary<string, OutdatedStatus> OutdatedStats { get; set; } = new();

    public DateTime OutdatedRequestCreatedAt { get; set; }

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }
}

public class OutdatedStatus
{
    public bool IsOutdated { get; set; }
}
