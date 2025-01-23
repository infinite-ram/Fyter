using AutoMapper;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.Plugins.InMemory;

public class FighterRepository : IFighterRepository
{
    private List<Fighter> _fighters;
    private readonly IMapper _mapper;

    public FighterRepository()
    {
        _fighters = GenerateInMemoryFighters.Execute();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = config.CreateMapper();
    }

    public Task AddFighterAsync(Fighter fighter)
    {
        _fighters.Add(fighter);

        return Task.CompletedTask;
    }

    public Task<Fighter> GetFighterByIdAsync(int fighterId)
    {
        var fighter = _fighters.FirstOrDefault(f => f.FighterId == fighterId);
        if (fighter != null)
            return Task.FromResult(fighter);

        return null;
    }

    public async Task<List<Fighter>> GetFightersByNameAsync(string fighterName)
    {
        if (string.IsNullOrWhiteSpace(fighterName))
            return await Task.FromResult(_fighters);

        return _fighters
            .Where(x => x.GetFullName().Contains(fighterName, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public Task UpdateFighterAsync(Fighter fighter)
    {
        if (
            _fighters.Any(f =>
                f.FighterId != fighter.FighterId
                && f.GetFullName()
                    .Contains(fighter.GetFullName(), StringComparison.OrdinalIgnoreCase)
            )
        )
            return Task.CompletedTask;

        var fighterToUpdate = _fighters.FirstOrDefault(f => f.FighterId == fighter.FighterId);
        if (fighterToUpdate != null)
        {
            _mapper.Map(fighter, fighterToUpdate);
        }

        return Task.CompletedTask;
    }

    public Task DeleteFighterAsync(Fighter fighter)
    {
        var fighterToBeDeleted = _fighters.FirstOrDefault(f => f.FighterId == fighter.FighterId);
        if (fighterToBeDeleted != null)
            _fighters.Remove(fighterToBeDeleted);

        return Task.CompletedTask;
    }

    public Task<List<Fighter>> GetAllBaseFightersAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAlterEgoAsync(int baseFighterId, Fighter alterEgo)
    {
        throw new NotImplementedException();
    }

    public Task<List<Fighter>> GetBaseFightersAsync(string searchQuery = "", int? limit = null)
    {
        throw new NotImplementedException();
    }

    public Task SetOutdatedFighterStatsAsync(Fighter fighter)
    {
        throw new NotImplementedException();
    }

    public Task<List<Fighter>> GetOutdatedFightersAsync(string fighterName)
    {
        throw new NotImplementedException();
    }

    public Task SetUpdatedFighterStatsAsync(Fighter fighter)
    {
        throw new NotImplementedException();
    }

    public Task SetOutdatedFighterStatsAsync(Fighter fighter, string userId)
    {
        throw new NotImplementedException();
    }
}
