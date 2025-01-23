using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Fyter.WebApp.DTOs;

namespace Fyter.WebApp.Components.Controls.AddFighterFormComponents;

public partial class AddFighterTopMovesFormComponent : ComponentBase
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

    private void AddTopMove()
    {
        var newTopMove = new TopMovesDto { MoveName = string.Empty, Stars = 0.0 };
        _fighterDto.TopMoves.Add(newTopMove);
    }

    private void RemoveTopMove(TopMovesDto topMove)
    {
        {
            _fighterDto.TopMoves.Remove(topMove);
        }
    }
}
