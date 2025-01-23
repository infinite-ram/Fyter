using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;

namespace Fyter.WebApp.Components.Controls.FighterDetailComponents;

public partial class FighterTopMovesDetailComponent : ComponentBase
{
    [Parameter]
    public required Fighter Fighter { get; set; }
}