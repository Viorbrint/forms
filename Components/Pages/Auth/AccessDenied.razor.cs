using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Forms.Components.Pages.Auth;

public partial class AccessDenied : ComponentBase
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; } = null!;
    
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;
    
    private async Task NavigateBack()
    {
        await JSRuntime.InvokeVoidAsync("history.back");
    }

    private void NavigateHome()
    {
        NavigationManager.NavigateTo("/");
    }
}