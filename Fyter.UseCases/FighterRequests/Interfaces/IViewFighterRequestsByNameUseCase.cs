using Fyter.CoreBusiness.FighterRequestModel;

namespace Fyter.UseCases.FighterRequests.Interfaces;

public interface IViewFighterRequestsByNameUseCase
{
    Task<List<FighterRequest>> ExecuteAsync(string fighterName);
}
