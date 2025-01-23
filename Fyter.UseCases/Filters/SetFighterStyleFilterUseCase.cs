using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Filters.Interfaces;

namespace Fyter.UseCases.Filters;

public class SetFighterStyleFilterUseCase : ISetFighterStyleFilterUseCase
{
    private readonly IFighterFilterRepository fighterFilterRepository;

    public SetFighterStyleFilterUseCase(IFighterFilterRepository fighterFilterRepository)
    {
        this.fighterFilterRepository = fighterFilterRepository;
    }

    public async Task ExecuteAsync(FighterStyleEnum? fighterStyle)
    {
        await this.fighterFilterRepository.SetFighterStyleFilter(fighterStyle);
    }
}
