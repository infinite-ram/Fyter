using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Filters.Interfaces;

public interface ISetFighterDivisionFilterUseCase
{
    Task ExecuteAsync(DivisionEnum? division);
}
