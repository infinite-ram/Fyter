using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Filters.Interfaces;

public interface IGetFightersAsQueryableUseCase
{
    Task<IQueryable<Fighter>> ExecuteAsync();
}
