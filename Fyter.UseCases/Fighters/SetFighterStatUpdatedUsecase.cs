using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;

namespace Fyter.UseCases.Fighters;

public class SetFighterStatUpdatedUsecase : ISetFighterStatUpdatedUsecase
{
    private readonly IFighterRepository fighterRepository;

    public SetFighterStatUpdatedUsecase(IFighterRepository fighterRepository)
    {
        this.fighterRepository = fighterRepository;
    }

    public async Task ExecuteAsync(Fighter fighter)
    {
        await fighterRepository.SetUpdatedFighterStatsAsync(fighter);
    }
}
