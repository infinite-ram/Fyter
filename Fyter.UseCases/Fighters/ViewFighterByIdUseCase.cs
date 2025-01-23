using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.UseCases.Fighters;

public class ViewFighterByIdUseCase : IViewFighterByIdUseCase
{
    private readonly IFighterRepository fighterRepository;

    public ViewFighterByIdUseCase(IFighterRepository fighterRepository)
    {
        this.fighterRepository = fighterRepository;
    }

    public async Task<Fighter> ExecuteAsync(int fighterId)
    {
        return await fighterRepository.GetFighterByIdAsync(fighterId);
    }
}
