using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Fighters.Interfaces;

public interface IUpdateFighterUseCase
{
    Task ExecuteAsync(Fighter fighter);
}
