using Fyter.CoreBusiness.FighterRequestModel;

namespace Fyter.UseCases.FighterRequests.Interfaces;

public interface IUpdateFighterRequestUseCase
{
    Task ExecuteAsync(FighterRequest fighterRequest);
}
