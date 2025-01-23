using Fyter.CoreBusiness.FighterModel;
using Fyter.WebApp.DTOs;

namespace Fyter.WebApp.Services.Interfaces;

public interface IFighterValidationService
{
    Task ValidateFighterFullNameAsync(FighterDto fighterDto);
    Task ValidateFighterAlterEgoNameAsync(Fighter? baseFighter, FighterDto fighterDto);
}
