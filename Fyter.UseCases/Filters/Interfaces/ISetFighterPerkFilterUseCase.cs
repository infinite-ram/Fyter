using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Filters.Interfaces;

public interface ISetFighterPerkFilterUseCase
{
    Task ExecuteAsync(PerksEnum? perk);
}
