using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Filters.Interfaces;

namespace Fyter.UseCases.Filters;

public class SetFightersAsQueryableUseCase : ISetFightersAsQueryableUseCase
{
    private readonly IFighterFilterRepository fighterFilterRepository;

    public SetFightersAsQueryableUseCase(IFighterFilterRepository fighterFilterRepository)
    {
        this.fighterFilterRepository = fighterFilterRepository;
    }

    public async Task ExecuteAsync(IQueryable<Fighter> query)
    {
        await this.fighterFilterRepository.SetFightersAsQueryable(query);
    }
}
