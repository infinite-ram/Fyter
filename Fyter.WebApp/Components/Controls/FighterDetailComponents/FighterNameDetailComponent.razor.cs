using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;

namespace Fyter.WebApp.Components.Controls.FighterDetailComponents;

public partial class FighterNameDetailComponent : ComponentBase
{
    [Parameter]
    public required Fighter Fighter { get; set; }

    protected override void OnParametersSet()
    {
        Fighter.FirstName = CapitalizeFirstLetter(Fighter.FirstName);
        Fighter.LastName = CapitalizeFirstLetter(Fighter.LastName);
    }

    static string CapitalizeFirstLetter(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToUpper(input[0]) + input.Substring(1);
    }
}
