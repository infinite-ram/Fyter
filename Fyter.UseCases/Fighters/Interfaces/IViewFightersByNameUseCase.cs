using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Fighters.Interfaces;

public interface IViewFightersByNameUseCase
{
    Task<List<Fighter>> ExecuteAsync(string fighterName);
}
