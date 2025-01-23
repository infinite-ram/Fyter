using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Filters.Interfaces;

public interface ISetFighterStyleFilterUseCase
{
    Task ExecuteAsync(FighterStyleEnum? fighterStyle);
}
