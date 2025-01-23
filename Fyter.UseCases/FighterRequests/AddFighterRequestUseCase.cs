using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.FighterRequestModel;
using Fyter.UseCases.FighterRequests.Interfaces;

namespace Fyter.UseCases.FighterRequests;

public class AddFighterRequestUseCase : IAddFighterRequestUseCase
{
    private readonly IFighterRequestRepository fighterRequestRepository;

    public AddFighterRequestUseCase(IFighterRequestRepository fighterRequestRepository)
    {
        this.fighterRequestRepository = fighterRequestRepository;
    }

    public async Task ExecuteAsync(FighterRequest fighterRequest)
    {
        await fighterRequestRepository.AddFighterRequestAsync(fighterRequest);
    }
}
