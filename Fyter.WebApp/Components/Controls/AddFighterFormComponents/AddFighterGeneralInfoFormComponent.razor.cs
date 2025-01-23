using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Fyter.CoreBusiness.Exceptions;
using Fyter.WebApp.DTOs;
using Fyter.WebApp.Services;
using Fyter.WebApp.Services.Interfaces;

namespace Fyter.WebApp.Components.Controls.AddFighterFormComponents;

public partial class AddFighterGeneralInfoFormComponent : ComponentBase, IDisposable
{
    [Inject]
    public required IAddFighterFormService AddFighterFormService { get; set; }

    [Inject]
    public required IFighterValidationService FighterValidationService { get; set; }

    [CascadingParameter]
    public EditContext editContext { get; set; } = default!;

    [CascadingParameter]
    public ValidationMessageStore messageStore { get; set; } = default!;

    private FighterDto _fighterDto;

    protected override void OnParametersSet()
    {
        editContext.OnFieldChanged -= OnFieldChanged;

        if (editContext.Model is FighterDto model)
            _fighterDto = model;

        editContext.OnFieldChanged += OnFieldChanged;
    }

    private async void OnFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        ClearPreviousFullNameValidations();

        try
        {
            await FighterValidationService.ValidateFighterFullNameAsync(_fighterDto);
        }
        catch (FighterNameAlreadyExistsException ex)
        {
            ApplyFullNameValidations(ex.Message);

            editContext.NotifyValidationStateChanged();
        }
    }

    private void ApplyFullNameValidations(string message)
    {
        messageStore.Add(
            new FieldIdentifier(_fighterDto, nameof(FighterDto.FirstName)),
            $"'{_fighterDto.GetFullName()}' already exists!"
        );
        messageStore.Add(
            new FieldIdentifier(_fighterDto, nameof(FighterDto.LastName)),
            $"'{_fighterDto.GetFullName()}' already exists!"
        );

        messageStore.Add(new FieldIdentifier(_fighterDto, string.Empty), message);
    }

    private void ClearPreviousFullNameValidations()
    {
        messageStore.Clear(
            new FieldIdentifier(_fighterDto, nameof(FighterDto.FirstName))
        );
        messageStore.Clear(
            new FieldIdentifier(_fighterDto, nameof(FighterDto.LastName))
        );
        messageStore.Clear(new FieldIdentifier(_fighterDto, string.Empty));
    }

    public void Dispose()
    {
        editContext.OnFieldChanged -= OnFieldChanged;
    }
}
