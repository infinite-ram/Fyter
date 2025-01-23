using Microsoft.AspNetCore.Components;

namespace Fyter.WebApp.Components.Common;

public partial class DisplayFighterStars : ComponentBase
{
    [Parameter]
    public double Stars { get; set; }

    [Parameter]
    public bool IsOutedated { get; set; }
}
