using Fyter.CoreBusiness.FighterRequestModel;

namespace Fyter.UseCases.FighterRequests.Interfaces;

public interface IDeleteFighterRequestUseCase
{
    Task ExecuteAsync(FighterRequest fighterRequest);
}
