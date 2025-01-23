using Fyter.CoreBusiness.Filters;

namespace Fyter.UseCases.Filters.Interfaces;

public interface ISetStandUpFilterUseCase
{
    Task ExecuteAsync(FighterStandUpFilter? standUpFilters);
}
