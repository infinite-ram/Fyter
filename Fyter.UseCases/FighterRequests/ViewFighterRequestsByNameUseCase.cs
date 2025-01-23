using Fyter.CoreBusiness.FighterRequestModel;
using Fyter.UseCases.FighterRequests.Interfaces;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.UseCases.FighterRequests;

public class ViewFighterRequestsByNameUseCase : IViewFighterRequestsByNameUseCase
{
    private readonly IFighterRequestRepository fighterRequestRepository;

    public ViewFighterRequestsByNameUseCase(IFighterRequestRepository fighterRequestRepository)
    {
        this.fighterRequestRepository = fighterRequestRepository;
    }

    public async Task<List<FighterRequest>> ExecuteAsync(string fighterName)
    {
        return await fighterRequestRepository.GetFighterRequestsByNameAsync(fighterName);
    }
}
