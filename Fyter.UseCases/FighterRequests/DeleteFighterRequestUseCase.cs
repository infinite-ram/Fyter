using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.FighterRequestModel;
using Fyter.UseCases.FighterRequests.Interfaces;

namespace Fyter.UseCases.FighterRequests;

public class DeleteFighterRequestUseCase : IDeleteFighterRequestUseCase
{
    private readonly IFighterRequestRepository fighterRequestRepository;

    public DeleteFighterRequestUseCase(IFighterRequestRepository fighterRequestRepository)
    {
        this.fighterRequestRepository = fighterRequestRepository;
    }

    public async Task ExecuteAsync(FighterRequest fighterRequest)
    {
        await fighterRequestRepository.DeleteFighterAsync(fighterRequest);
    }
}
