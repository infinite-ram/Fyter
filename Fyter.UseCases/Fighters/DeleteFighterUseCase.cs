using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;

namespace Fyter.UseCases.Fighters;

public class DeleteFighterUseCase : IDeleteFighterUseCase
{
    private readonly IFighterRepository fighterRepository;

    public DeleteFighterUseCase(IFighterRepository fighterRepository)
    {
        this.fighterRepository = fighterRepository;
    }

    public async Task ExecuteAsync(Fighter fighter)
    {
        await fighterRepository.DeleteFighterAsync(fighter);
    }
}
