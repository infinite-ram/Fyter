using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.Filters;
using Fyter.UseCases.Filters.Interfaces;

namespace Fyter.UseCases.Filters;

public class SetStandUpFilterUseCase : ISetStandUpFilterUseCase
{
    private readonly IFighterFilterRepository fighterFilterRepository;

    public SetStandUpFilterUseCase(IFighterFilterRepository fighterFilterRepository)
    {
        this.fighterFilterRepository = fighterFilterRepository;
    }

    public async Task ExecuteAsync(FighterStandUpFilter? standUpFilters)
    {
        await this.fighterFilterRepository.SetStandUpFilter(standUpFilters);
    }
}
