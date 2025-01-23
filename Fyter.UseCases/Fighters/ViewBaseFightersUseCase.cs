using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.UseCases.Fighters;

public class ViewBaseFightersUseCase : IViewBaseFightersUseCase
{
    private readonly IFighterRepository fighterRepository;

    public ViewBaseFightersUseCase(IFighterRepository fighterRepository)
    {
        this.fighterRepository = fighterRepository;
    }

    public async Task<List<Fighter>> ExecuteAsync(string searchQuery = "", int? limit = null)
    {
        return await fighterRepository.GetBaseFightersAsync(searchQuery, limit);
    }
}
