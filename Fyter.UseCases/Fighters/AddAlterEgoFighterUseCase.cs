using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;

namespace Fyter.UseCases.Fighters;

public class AddAlterEgoFighterUseCase : IAddAlterEgoFighterUseCase
{
    private readonly IFighterRepository fighterRepository;

    public AddAlterEgoFighterUseCase(IFighterRepository fighterRepository)
    {
        this.fighterRepository = fighterRepository;
    }

    public async Task ExecuteAsync(int baseFighterId, Fighter alterEgo)
    {
        await fighterRepository.AddAlterEgoAsync(baseFighterId, alterEgo);
    }
}
