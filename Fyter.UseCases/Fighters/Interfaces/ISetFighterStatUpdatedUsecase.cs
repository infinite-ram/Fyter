using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Fighters.Interfaces;

public interface ISetFighterStatUpdatedUsecase
{
    Task ExecuteAsync(Fighter fighter);
}
