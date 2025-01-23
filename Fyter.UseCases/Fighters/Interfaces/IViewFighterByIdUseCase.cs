using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Fighters.Interfaces;

public interface IViewFighterByIdUseCase
{
    Task<Fighter> ExecuteAsync(int fighterId);
}
