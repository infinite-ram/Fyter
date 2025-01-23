using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;
using Fyter.WebApp.Services;
using Fyter.WebApp.Services.Interfaces;
using Fyter.WebApp.Utilities;

namespace Fyter.WebApp.Components.Controls.AddFighterFormComponents;

public partial class IsFighterAlterEgoComponent : ComponentBase
{
    [Inject]
    public required IAddFighterFormService AddFighterFormService { get; set; }

    [Inject]
    public IFormDataCacheService FormDataCacheService { get; set; }

    public bool IsAlterEgo
    {
        get => AddFighterFormService.IsAlterEgo;
        set
        {
            if (AddFighterFormService.IsAlterEgo != value)
            {
                AddFighterFormService.IsAlterEgo = value;
                selectedBaseFighter = null;
            }
        }
    }

    private int? baseFighterId;
    private Fighter? selectedBaseFighter;

    private const string BaseFighterFormCacheKey = "AddBaseFighterFormData";

    protected override void OnInitialized()
    {
        if (selectedBaseFighter == null)
        {
            selectedBaseFighter = AddFighterFormService.GetBaseFighter();
        }
    }

    private async Task HandleFighterSelected(int? selectedFighterId)
    {
        selectedBaseFighter = await AddFighterFormService.SetBaseFighterAsync(
            selectedFighterId.Value
        );

        AddFighterFormService.Refresh();

        StateHasChanged();
    }
}
