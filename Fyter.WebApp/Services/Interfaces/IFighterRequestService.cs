using Fyter.WebApp.DTOs;

namespace Fyter.WebApp.Services.Interfaces;

public interface IFighterRequestService
{
    Task SetFighterRequestAsync(int fighterRequestId, FighterDto fighter);
    Task UpdateFighterRequestAsync(int fighterRequestId);
}
