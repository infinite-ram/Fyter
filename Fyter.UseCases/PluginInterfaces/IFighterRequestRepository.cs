using Fyter.CoreBusiness.FighterRequestModel;

namespace Fyter.UseCases.PluginInterfaces;

public interface IFighterRequestRepository
{
    Task AddFighterRequestAsync(FighterRequest fighter);
    Task DeleteFighterAsync(FighterRequest fighter);
    Task<bool> IsNameExistsAsync(string fullName, int excludeId = 0);

    Task<FighterRequest> GetFighterRequestByIdAsync(int fighterId);
    Task<List<FighterRequest>> GetFighterRequestsByNameAsync(string fighterName);
    Task UpdateFighterRequestAsync(FighterRequest fighter);
}
