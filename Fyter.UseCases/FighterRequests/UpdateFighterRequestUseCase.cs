using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.FighterRequestModel;
using Fyter.UseCases.FighterRequests.Interfaces;

namespace Fyter.UseCases.FighterRequests;

public class UpdateFighterRequestUseCase : IUpdateFighterRequestUseCase
{
    private readonly IFighterRequestRepository fighterRequestRepository;

    public UpdateFighterRequestUseCase(IFighterRequestRepository fighterRequestRepository)
    {
        this.fighterRequestRepository = fighterRequestRepository;
    }

    public async Task ExecuteAsync(FighterRequest fighterRequest)
    {
        await fighterRequestRepository.UpdateFighterRequestAsync(fighterRequest);
    }
}
