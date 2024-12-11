using Forms.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace Forms.Components.Pages.Auth;

public partial class Signin : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IAuthService AuthService { get; set; } = null!;

    public SigninModel signinModel = new();

    private async Task HandleValidSubmit() { }

    private void NavigateToMain()
    {
        NavigationManager.NavigateTo("/");
    }

    private void NavigateToSignup()
    {
        NavigationManager.NavigateTo("/signup");
    }
}
