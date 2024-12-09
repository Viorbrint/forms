using Forms.Services;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.Auth;

public partial class Logout : ComponentBase
{
    [Inject]
    private IAuthService AuthService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await AuthService.Logout();
        NavigationManager.NavigateTo("/signin");
    }
}
