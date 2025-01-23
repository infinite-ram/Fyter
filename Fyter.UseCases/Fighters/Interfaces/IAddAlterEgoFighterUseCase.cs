using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Fighters.Interfaces;

public interface IAddAlterEgoFighterUseCase
{
    Task ExecuteAsync(int baseFighterId, Fighter alterEgo);
}
