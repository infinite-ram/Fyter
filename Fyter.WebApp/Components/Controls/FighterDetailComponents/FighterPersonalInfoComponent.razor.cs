using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;

namespace Fyter.WebApp.Components.Controls.FighterDetailComponents;

public partial class FighterPersonalInfoComponent : ComponentBase
{
    [Inject]
    public required NavigationManager NavigationManager { get; set; }

    [Parameter]
    public Fighter? Fighter { get; set; }

    private string? height;
    private string? outdatedHeightPropPath;

    protected override void OnParametersSet()
    {
        if (Fighter != null)
        {
            var feet = Fighter.FighterInfo.Height.Feet;
            var inches = Fighter.FighterInfo.Height.Inches;
            height = $"{feet}'{inches}\"";

            DetermineHeightOutdatedPropPath();
        }
    }

    private void DetermineHeightOutdatedPropPath()
    {
        string feetHeight = "FighterInfo.Height.Feet";
        string inchesHeight = "FighterInfo.Height.Inches";

        if (Fighter != null)
        {
            if (Fighter.OutdatedStats.TryGetValue(feetHeight, out var status) && status.IsOutdated)
            {
                outdatedHeightPropPath = feetHeight;
            }
            else if (
                Fighter.OutdatedStats.TryGetValue(inchesHeight, out var inchesStatus)
                && inchesStatus.IsOutdated
            )
            {
                outdatedHeightPropPath = inchesHeight;
            }
        }
    }
}
