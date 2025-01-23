using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Fighters.Interfaces;

public interface IViewOutdatedFightersByNameUseCase
{
    Task<List<Fighter>> ExecuteAsync(string fighterName);
}
