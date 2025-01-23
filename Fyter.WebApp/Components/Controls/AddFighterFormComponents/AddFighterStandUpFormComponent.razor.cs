using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Fyter.WebApp.DTOs;

namespace Fyter.WebApp.Components.Controls.AddFighterFormComponents;

public partial class AddFighterStandUpFormComponent : ComponentBase
{
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
        editContext.NotifyValidationStateChanged();
    }
}
