using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Fighters.Interfaces;

public interface IDeleteFighterUseCase
{
    Task ExecuteAsync(Fighter fighter);
}
