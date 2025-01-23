using Fyter.CoreBusiness.FighterRequestModel;

namespace Fyter.UseCases.FighterRequests.Interfaces;

public interface IAddFighterRequestUseCase
{
    Task ExecuteAsync(FighterRequest fighterRequest);
}
