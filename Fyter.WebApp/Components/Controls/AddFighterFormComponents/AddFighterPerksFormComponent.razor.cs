using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Fyter.CoreBusiness.FighterModel;
using Fyter.WebApp.DTOs;
using Fyter.WebApp.Extensions;

namespace Fyter.WebApp.Components.Controls.AddFighterFormComponents;

public partial class AddFighterPerksFormComponent : ComponentBase
{
    [CascadingParameter]
    public EditContext editContext { get; set; } = default!;

    [CascadingParameter]
    public ValidationMessageStore messageStore { get; set; } = default!;

    private FighterDto _fighterDto;

    private string searchTerm = string.Empty;

    private List<PerksEnum> FilteredPerks =>
        Enum.GetValues<PerksEnum>()
            .Where(perk =>
                perk.GetDisplayName().Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            )
            .ToList();

    protected override void OnParametersSet()
    {
        if (editContext.Model is FighterDto model)
            _fighterDto = model;
    }

    private void TogglePerkSelection(PerksEnum perk)
    {
        if (_fighterDto.Perks.Contains(perk))
            _fighterDto.Perks.Remove(perk);
        else
            _fighterDto.Perks.Add(perk);
    }
}
