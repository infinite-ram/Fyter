using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Fyter.CoreBusiness.FighterModel;

namespace Fyter.WebApp.Components.Controls.FighterDetailComponents;

public partial class FighterStarsAttributeComponent : ComponentBase
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; } = default!;

    [Parameter]
    public required string Label { get; set; }

    [Parameter]
    public required double Stars { get; set; }

    [Parameter]
    public int? Value { get; set; }

    [Parameter]
    public string? Col { get; set; }

    [Parameter]
    public string? OutOf { get; set; }

    [Parameter]
    public Fighter? Fighter { get; set; }

    [Parameter]
    public string? OutdatedProp { get; set; }

    [Parameter]
    public string? StarsRatingOutdatedProp { get; set; }

    [Parameter]
    public string? RatingOutdatedProp { get; set; }

    private bool isStarsOuted;
    private bool isRatingOuted;
    private bool isAttributeOuted;

    private string? outdatedColor;

    private string ComputeCol()
    {
        return !string.IsNullOrEmpty(Col) ? $"col-12 col-md-{Col}" : "col-12 col-md-5";
    }

    private string ComputeOutOf()
    {
        return !string.IsNullOrEmpty(OutOf) ? $"/{OutOf}" : "/100";
    }

    private void HandleOutdated(string? prop)
    {
        if (
            !string.IsNullOrWhiteSpace(StarsRatingOutdatedProp)
            && StarsRatingOutdatedProp.Equals(prop)
        )
        {
            isStarsOuted = true;
        }
        else
        {
            isStarsOuted = false;
        }

        if (!string.IsNullOrWhiteSpace(RatingOutdatedProp) && RatingOutdatedProp.Equals(prop))
        {
            isRatingOuted = true;
            outdatedColor = "text-warning";
        }
        else
        {
            outdatedColor = "";
        }

        if (!string.IsNullOrWhiteSpace(OutdatedProp) && OutdatedProp.Equals(prop))
        {
            isAttributeOuted = true;
        }
        else
        {
            isAttributeOuted = false;
        }
    }
}
