using Fyter.UseCases.PluginInterfaces;
using Fyter.CoreBusiness.FighterRequestModel;
using Fyter.UseCases.Filters.Interfaces;

namespace Fyter.UseCases.Filters;

public class SetFighterStatusFilterUseCase : ISetFighterStatusFilterUseCase
{
    private readonly IFighterStatusFilterRepository fighterFilterRepository;

    public SetFighterStatusFilterUseCase(IFighterStatusFilterRepository fighterFilterRepository)
    {
        this.fighterFilterRepository = fighterFilterRepository;
    }

    public async Task ExecuteAsync(FighterUpdateStatusEnum? status)
    {
        await this.fighterFilterRepository.SetFighterStatusFilter(status);
    }
}
