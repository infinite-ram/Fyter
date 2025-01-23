using Microsoft.AspNetCore.Components;
using Fyter.WebApp.Utilities;

namespace Fyter.WebApp.Components.Pages;

public partial class Testing : ComponentBase
{
    protected override async Task OnInitializedAsync() { }

    private void Clear()
    {
        DeveloperUtilities.ClearLogs();
    }
}
