using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Filters.Interfaces;

namespace Fyter.UseCases.Filters;

public class SetFighterPerkFilterUseCase : ISetFighterPerkFilterUseCase
{
    private readonly IFighterFilterRepository fighterFilterRepository;

    public SetFighterPerkFilterUseCase(IFighterFilterRepository fighterFilterRepository)
    {
        this.fighterFilterRepository = fighterFilterRepository;
    }

    public async Task ExecuteAsync(PerksEnum? perk)
    {
        await this.fighterFilterRepository.SetFighterPerkFilter(perk);
    }
}
