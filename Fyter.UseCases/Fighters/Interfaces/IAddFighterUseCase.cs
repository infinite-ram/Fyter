using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Fighters.Interfaces;

public interface IAddFighterUseCase
{
    Task ExecuteAsync(Fighter fighter);
}
