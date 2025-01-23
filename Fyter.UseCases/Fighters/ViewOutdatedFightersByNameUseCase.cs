using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.UseCases.Fighters;

public class ViewOutdatedFightersByNameUseCase : IViewOutdatedFightersByNameUseCase
{
    private readonly IFighterRepository fighterRepository;

    public ViewOutdatedFightersByNameUseCase(IFighterRepository fighterRepository)
    {
        this.fighterRepository = fighterRepository;
    }

    public async Task<List<Fighter>> ExecuteAsync(string fighterName)
    {
        return await fighterRepository.GetOutdatedFightersAsync(fighterName);
    }
}
