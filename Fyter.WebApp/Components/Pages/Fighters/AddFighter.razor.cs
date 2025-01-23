using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Fyter.CoreBusiness.Exceptions;
using Fyter.CoreBusiness.FighterModel;
using Fyter.CoreBusiness.FighterRequestModel;
using Fyter.UseCases.Fighters.Interfaces;
using Fyter.WebApp.DTOs;
using Fyter.WebApp.Services;
using Fyter.WebApp.Services.Interfaces;

namespace Fyter.WebApp.Components.Pages.Fighters;

public partial class AddFighter : ComponentBase, IDisposable
{
    [Inject]
    public required IAddFighterFormService AddFighterFormService { get; set; }

    [Inject]
    public required IFighterService FighterService { get; set; }

    [Inject]
    public required IFighterRequestService FighterRequestService { get; set; }

    [Inject]
    public required IFighterValidationService FighterValidationService { get; set; }

    [Inject]
    public required IViewFighterByIdUseCase ViewFighterByIdUseCase { get; set; }

    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Inject]
    public required IMapper Mapper { get; set; }

    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    [Parameter]
    public int? FighterRequestId { get; set; }

    private FighterRequest? fighterRequest;

    private bool isAlterEgo = false;

    private FighterDto _fighterDto;
    private Fighter? baseFighter;

    private List<FighterDto> baseFighters = new List<FighterDto>();

    private EditContext editContext;
    private ValidationMessageStore messageStore;

    protected override async Task OnInitializedAsync()
    {
        AddFighterFormService.OnStateChanged -= Refresh;
        AddFighterFormService.OnStateChanged += Refresh;

        _fighterDto = AddFighterFormService.GetFighterViewModel();

        InitializeEditContext();

        baseFighters = await FighterService.GetBaseFightersAsync();
    }

    protected override async Task OnParametersSetAsync()
    {
        if (FighterRequestId.HasValue)
        {
            await FighterRequestService.SetFighterRequestAsync(FighterRequestId.Value, _fighterDto);
        }
    }

    private void InitializeEditContext()
    {
        if (editContext != null)
            editContext.OnFieldChanged -= OnFieldChanged;

        editContext = new EditContext(_fighterDto);
        messageStore = new ValidationMessageStore(editContext);
        editContext.OnFieldChanged += OnFieldChanged;
    }

    private void OnFieldChanged(object sender, FieldChangedEventArgs args)
    {
        // Clear validation messages for the field that changed
        messageStore.Clear(args.FieldIdentifier);
        editContext.NotifyValidationStateChanged();

        StateHasChanged();
    }

    private void ClearForm()
    {
        AddFighterFormService.ResetFighter();
        AddFighterFormService.Refresh();

        messageStore.Clear(new FieldIdentifier(_fighterDto, string.Empty));

        editContext.NotifyValidationStateChanged();
        StateHasChanged();
    }

    private async Task HandleValidSubmit()
    {
        try
        {
            await FighterValidationService.ValidateFighterFullNameAsync(_fighterDto);
            await FighterValidationService.ValidateFighterAlterEgoNameAsync(
                baseFighter,
                _fighterDto
            );
            await FighterService.SetUserIdToFighterAsync(_fighterDto);
            await FighterService.AddFighterAsync(_fighterDto);

            if (FighterRequestId.HasValue)
                await FighterRequestService.UpdateFighterRequestAsync(FighterRequestId.Value);

            AddFighterFormService.ResetFighter();

            NavigationManager.NavigateTo("/");
        }
        catch (FighterNameAlreadyExistsException ex)
        {
            messageStore.Add(new FieldIdentifier(_fighterDto, string.Empty), ex.Message);
            editContext.NotifyValidationStateChanged();
        }
        catch (FighterAlterEgoNameException ex)
        {
            messageStore.Add(
                new FieldIdentifier(_fighterDto, nameof(FighterDto.EgoName)),
                ex.Message
            );
            editContext.NotifyValidationStateChanged();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error adding fighter: {ex.Message}");
        }
    }

    private async Task HandleInvalidSubmit()
    {
        // Add a short delay to allow Blazor to fully render the validation messages
        await Task.Delay(50);

        // Call the JavaScript function to scroll to the first validation error
        await JSRuntime.InvokeVoidAsync("scrollToFirstError");
    }

    private void Refresh()
    {
        _fighterDto = AddFighterFormService.GetFighterViewModel();
        baseFighter = AddFighterFormService.GetBaseFighter();

        InitializeEditContext();
        StateHasChanged();
    }

    public void Dispose()
    {
        AddFighterFormService.OnStateChanged -= Refresh;
    }
}
