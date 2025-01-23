using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;

namespace Fyter.WebApp.Components.Controls.FighterDetailComponents;

public partial class FighterAttributeComponent : ComponentBase
{
    [Parameter]
    public required string Label { get; set; }

    [Parameter]
    public required string Attribute { get; set; }

    [Parameter]
    public string? Col { get; set; }

    [Parameter]
    public Fighter? Fighter { get; set; }

    [Parameter]
    public string? OutdatedProp { get; set; }

    private string ComputeCol()
    {
        return !string.IsNullOrEmpty(Col) ? $"col-{Col}" : "col-5";
    }
}
