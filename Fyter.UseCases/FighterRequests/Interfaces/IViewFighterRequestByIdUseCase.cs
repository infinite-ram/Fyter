using Fyter.CoreBusiness.FighterRequestModel;

namespace Fyter.UseCases.FighterRequests.Interfaces;

public interface IViewFighterRequestByIdUseCase
{
    Task<FighterRequest> ExecuteAsync(int fighterRequestId);
}
