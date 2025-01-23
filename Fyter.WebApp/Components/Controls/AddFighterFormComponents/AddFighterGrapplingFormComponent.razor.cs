using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Fyter.WebApp.DTOs;

namespace Fyter.WebApp.Components.Controls.AddFighterFormComponents;

public partial class AddFighterGrapplingFormComponent : ComponentBase
{
    [CascadingParameter]
    public EditContext editContext { get; set; } = default!;

    [CascadingParameter]
    public ValidationMessageStore messageStore { get; set; } = default!;

    private FighterDto _fighterDto;

    protected override void OnParametersSet()
    {
        if (editContext.Model is FighterDto model)
            _fighterDto = model;
    }
}
