using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Fighters.Interfaces;

public interface ISetFighterStatOutdatedUsecase
{
    Task ExecuteAsync(Fighter fighter, string userId);
}
