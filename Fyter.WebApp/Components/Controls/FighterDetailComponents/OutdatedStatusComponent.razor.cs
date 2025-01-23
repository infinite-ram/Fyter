using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Fyter.CoreBusiness.FighterModel;
using Fyter.WebApp.Utilities;

namespace Fyter.WebApp.Components.Controls.FighterDetailComponents;

public partial class OutdatedStatusComponent : ComponentBase
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; } = default!;

    [Parameter]
    public EventCallback<string> OnOutdated { get; set; }

    [Parameter]
    public required Fighter Fighter { get; set; }

    [Parameter]
    public string? PropertyPath { get; set; }

    [Parameter]
    public string? StarsRatingOutdatedProp { get; set; }

    [Parameter]
    public string? RatingOutdatedProp { get; set; }

    private bool isStatAttributeOutdated;
    private bool isStarsRatingOutdated;
    private bool isRatingOutdated;

    private bool oldStarsValue;
    private bool oldRatingValue;
    private bool oldPropValue;

    private string? toolTipTitle = "This Attribute is outdated!";

    protected override async Task OnParametersSetAsync()
    {
        DetermineOutdatedStat();

        if (!oldStarsValue && isStarsRatingOutdated)
        {
            await OnOutdated.InvokeAsync(StarsRatingOutdatedProp);
            ComputeToolTipTitle();
        }
        else if (!oldRatingValue && isRatingOutdated)
        {
            await OnOutdated.InvokeAsync(RatingOutdatedProp);
            ComputeToolTipTitle();
        }
        else if (!oldPropValue && isStatAttributeOutdated)
        {
            await OnOutdated.InvokeAsync(PropertyPath);
            ComputeToolTipTitle();
        }

        oldStarsValue = isStarsRatingOutdated;
        oldRatingValue = isRatingOutdated;
        oldPropValue = isStatAttributeOutdated;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeVoidAsync("addTooltips");
        }
    }

    private void ComputeToolTipTitle()
    {
        foreach (var stat in Fighter.OutdatedStats)
        {
            if (stat.Key.Equals(StarsRatingOutdatedProp))
            {
                var property = StringHelper.GetToolTipPropertyTitle(StarsRatingOutdatedProp);
                toolTipTitle = $"{property} is Outdated!";
            }
            if (stat.Key.Equals(RatingOutdatedProp))
            {
                var property = StringHelper.GetToolTipPropertyTitle(RatingOutdatedProp);
                toolTipTitle = $"{property} is Outdated!";
            }
            if (stat.Key.Equals(PropertyPath))
            {
                var property = StringHelper.GetToolTipPropertyTitle(PropertyPath);
                toolTipTitle = $"{property} is Outdated!";
            }
        }
    }

    private void DetermineOutdatedStat()
    {
        isStatAttributeOutdated = IsOutdated(PropertyPath);
        isStarsRatingOutdated = IsOutdated(StarsRatingOutdatedProp);
        isRatingOutdated = IsOutdated(RatingOutdatedProp);
    }

    private bool IsOutdated(string? propertyName)
    {
        if (Fighter != null)
        {
            return !string.IsNullOrWhiteSpace(propertyName)
                && Fighter.OutdatedStats.TryGetValue(propertyName, out var status)
                && status.IsOutdated;
        }

        return false;
    }
}
