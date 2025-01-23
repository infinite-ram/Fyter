using System.ComponentModel.DataAnnotations;

namespace Fyter.WebApp.DTOs;

public class HealthDto
{
    [ValidateComplexType]
    public StatDto Cardio { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto Chin { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto Body { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto Legs { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto Recovery { get; set; } = new StatDto();

    [ValidateComplexType]
    public StatDto CutResistant { get; set; } = new StatDto();
}
