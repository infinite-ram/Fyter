using Fyter.UseCases.Filters.Interfaces;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.UseCases.Filters;

public class ResetFilterQueryUseCase : IResetFilterQueryUseCase
{
    private readonly IFighterFilterRepository fighterFilterRepository;

    public ResetFilterQueryUseCase(IFighterFilterRepository fighterFilterRepository)
    {
        this.fighterFilterRepository = fighterFilterRepository;
    }

    public async Task ExecuteAsync()
    {
        await this.fighterFilterRepository.ResetQuery();
    }
}
