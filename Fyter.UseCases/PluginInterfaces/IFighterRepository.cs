using Fyter.CoreBusiness.FighterModel;

namespace Fyter.UseCases.PluginInterfaces;

public interface IFighterRepository
{
    Task AddAlterEgoAsync(int baseFighterId, Fighter alterEgo);
    Task AddFighterAsync(Fighter fighter);
    Task DeleteFighterAsync(Fighter fighter);
    Task<List<Fighter>> GetAllBaseFightersAsync();
    Task<Fighter> GetFighterByIdAsync(int fighterId);
    Task<List<Fighter>> GetFightersByNameAsync(string fighterName);
    Task<List<Fighter>> GetOutdatedFightersAsync(string fighterName);
    Task UpdateFighterAsync(Fighter fighter);
    Task<List<Fighter>> GetBaseFightersAsync(string searchQuery = "", int? limit = null);
    Task SetOutdatedFighterStatsAsync(Fighter fighter, string userId);
    Task SetUpdatedFighterStatsAsync(Fighter fighter);
}
