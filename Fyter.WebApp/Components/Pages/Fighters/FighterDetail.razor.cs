using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;

namespace Fyter.WebApp.Components.Pages.Fighters;

public partial class FighterDetail : ComponentBase
{
    [Inject]
    public required IViewFighterByIdUseCase ViewFighterByIdUseCase { get; set; }

    [Inject]
    public required ISetFighterStatOutdatedUsecase SetFighterStatOutdatedUsecase { get; set; }

    [Parameter]
    public int Id { get; set; }

    public Fighter? Fighter { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        Fighter = await ViewFighterByIdUseCase.ExecuteAsync(Id);
    }

    private void Refresh()
    {
        StateHasChanged();
    }
}
