using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;
using Fyter.CoreBusiness.FighterRequestModel;
using Fyter.UseCases.FighterRequests.Interfaces;
using Fyter.UseCases.PluginInterfaces;

namespace Fyter.WebApp.Components.Pages.FighterRequests;

public partial class EditFighterRequest : ComponentBase
{
    [Inject]
    public required IViewFighterRequestByIdUseCase ViewFighterRequestByIdUseCase { get; set; }

    [Inject]
    public required IUpdateFighterRequestUseCase UpdateFighterRequestUseCase { get; set; }

    [Inject]
    public required IFighterRequestRepository FighterRequestRepository { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    private FighterRequest FighterRequest = new FighterRequest();

    [Parameter]
    public int FighterRequestId { get; set; }

    private string? ValidationMessage;

    protected override async Task OnParametersSetAsync()
    {
        FighterRequest = await ViewFighterRequestByIdUseCase.ExecuteAsync(FighterRequestId);
    }

    private async Task Save()
    {
        ValidationMessage = null;

        var fullName = $"{FighterRequest.FirstName} {FighterRequest.LastName}";
        var nameExists = await FighterRequestRepository.IsNameExistsAsync(
            fullName,
            FighterRequest.Id
        );
        if (nameExists)
        {
            ValidationMessage = $"The name {fullName} already exists.";
            return;
        }

        var editedAtDate = DateTime.UtcNow;
        FighterRequest.DateEdited = editedAtDate;

        await UpdateFighterRequestUseCase.ExecuteAsync(FighterRequest);
        NavigationManager.NavigateTo("/fighterrequests");
    }

    private void HandleInvalidSubmit() { }
}
