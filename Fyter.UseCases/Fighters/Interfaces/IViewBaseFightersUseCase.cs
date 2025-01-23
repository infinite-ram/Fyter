using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.Fighters.Interfaces;

public interface IViewBaseFightersUseCase
{
    Task<List<Fighter>> ExecuteAsync(string searchQuery = "", int? limit = null);
}
