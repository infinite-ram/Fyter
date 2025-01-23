using Microsoft.AspNetCore.Components;

namespace Fyter.WebApp.Components.Controls.FighterDetailComponents;

public partial class FighterTopMoveAttributeComponent : ComponentBase
{
    [Parameter]
    public required string Label { get; set; }

    [Parameter]
    public required double Stars { get; set; }

    [Parameter]
    public required string MoveName { get; set; }

    [Parameter]
    public string? Col { get; set; }

    [Parameter]
    // determine out of how many stars? exampel 5 out of 10 (i.e. 5 /10)
    public string? OutOf { get; set; }

    private string ComputeCol()
    {
        return !string.IsNullOrEmpty(Col) ? $"col-{Col}" : "col-5";
    }

    private string ComputeOutOf()
    {
        return !string.IsNullOrEmpty(OutOf) ? $"/{OutOf}" : "/100";
    }
}
