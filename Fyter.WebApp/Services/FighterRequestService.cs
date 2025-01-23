using AutoMapper;
using Fyter.UseCases.FighterRequests.Interfaces;
using Fyter.WebApp.DTOs;
using Fyter.WebApp.Services.Interfaces;

namespace Fyter.WebApp.Services;

public class FighterRequestService : IFighterRequestService
{
    private readonly IUpdateFighterRequestUseCase _updateFighterRequest;
    private readonly IViewFighterRequestByIdUseCase _viewFighterRequest;
    private readonly IMapper _mapper;

    public FighterRequestService(
        IUpdateFighterRequestUseCase updateFighterRequest,
        IViewFighterRequestByIdUseCase viewFighterRequest,
        IMapper mapper
    )
    {
        _updateFighterRequest = updateFighterRequest;
        _viewFighterRequest = viewFighterRequest;
        _mapper = mapper;
    }

    public async Task SetFighterRequestAsync(int fighterRequestId, FighterDto fighter)
    {
        var fighterRequest = await _viewFighterRequest.ExecuteAsync(fighterRequestId);
        if (fighterRequest != null)
        {
            fighter.FirstName = fighterRequest.FirstName;
            fighter.LastName = fighterRequest.LastName;
        }
    }

    public async Task UpdateFighterRequestAsync(int fighterRequestId)
    {
        var fighterRequest = await _viewFighterRequest.ExecuteAsync(fighterRequestId);
        if (fighterRequest != null)
        {
            fighterRequest.HasBeenAdded = true;
            await _updateFighterRequest.ExecuteAsync(fighterRequest);
        }
    }
}
