
using Fyter.WebApp.DTOs;

namespace Fyter.WebApp.Services.Interfaces;

public interface IFighterService
{
    Task AddFighterAsync(FighterDto fighterDto);
    Task<List<FighterDto>> GetBaseFightersAsync();
    Task<FighterDto> GetFighterByIdAsync(int fighterId);
    Task<List<FighterDto>> GetFightersByNameAsync(string name);
    Task SetUserIdToFighterAsync(FighterDto fighter);
}
