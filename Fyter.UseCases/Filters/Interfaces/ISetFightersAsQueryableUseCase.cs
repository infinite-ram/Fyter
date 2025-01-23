using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Filters.Interfaces;

public interface ISetFightersAsQueryableUseCase
{
    Task ExecuteAsync(IQueryable<Fighter> query);
}
