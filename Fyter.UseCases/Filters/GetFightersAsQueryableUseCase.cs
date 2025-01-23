using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Filters.Interfaces;

namespace Fyter.UseCases.Filters;

public class GetFightersAsQueryableUseCase : IGetFightersAsQueryableUseCase
{
    private readonly IFighterFilterRepository fighterFilterRepository;

    public GetFightersAsQueryableUseCase(IFighterFilterRepository fighterFilterRepository)
    {
        this.fighterFilterRepository = fighterFilterRepository;
    }

    public async Task<IQueryable<Fighter>> ExecuteAsync()
    {
        return await this.fighterFilterRepository.GetFightersAsQueryable();
    }
}
