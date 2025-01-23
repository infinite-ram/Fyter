using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;

namespace Fyter.UseCases.Fighters;

public class SetFighterStatOutdatedUsecase : ISetFighterStatOutdatedUsecase
{
    private readonly IFighterRepository fighterRepository;

    public SetFighterStatOutdatedUsecase(IFighterRepository fighterRepository)
    {
        this.fighterRepository = fighterRepository;
    }

    public async Task ExecuteAsync(Fighter fighter, string userId)
    {
        await fighterRepository.SetOutdatedFighterStatsAsync(fighter, userId);
    }
}
