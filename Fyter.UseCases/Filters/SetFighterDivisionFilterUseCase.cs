using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Filters.Interfaces;

namespace Fyter.UseCases.Filters;

public class SetFighterDivisionFilterUseCase : ISetFighterDivisionFilterUseCase
{
    private readonly IFighterFilterRepository fighterFilterRepository;

    public SetFighterDivisionFilterUseCase(IFighterFilterRepository fighterFilterRepository)
    {
        this.fighterFilterRepository = fighterFilterRepository;
    }

    public async Task ExecuteAsync(DivisionEnum? division)
    {
        await this.fighterFilterRepository.SetFighterDivisionFilter(division);
    }
}
