using Fyter.WebApp.DTOs;

namespace Fyter.WebApp.Services.Interfaces;

public interface IAddFighterService
{
    Task AddFighterAsync(FighterDto dto);
}