using Fyter.CoreBusiness.FighterRequestModel;
using Fyter.UseCases.FighterRequests.Interfaces;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.UseCases.FighterRequests;

public class ViewFighterRequestByIdUseCase : IViewFighterRequestByIdUseCase
{
    private readonly IFighterRequestRepository fighterRequestRepository;

    public ViewFighterRequestByIdUseCase(IFighterRequestRepository fighterRequestRepository)
    {
        this.fighterRequestRepository = fighterRequestRepository;
    }

    public async Task<FighterRequest> ExecuteAsync(int fighterRequestId)
    {
        return await fighterRequestRepository.GetFighterRequestByIdAsync(fighterRequestId);
    }
}
