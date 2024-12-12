using Forms.Services;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.Auth;

public partial class Signin : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IAuthService AuthService { get; set; } = null!;

    public SigninModel signinModel = new();

    private async Task HandleValidSubmit()
    {
        var checkResult = await AuthService.CheckSignin(signinModel.Email, signinModel.Password);
        if (checkResult.Succeeded)
        {
            var key = Guid.NewGuid();
            CookieLoginMiddleware.Logins.Add(key, signinModel);
            NavigationManager.NavigateTo($"/signin?key={key}", true);
        }
        else
        {
            // TODO: Handle error
        }
    }

    private void NavigateToMain()
    {
        NavigationManager.NavigateTo("/");
    }

    private void NavigateToSignup()
    {
        NavigationManager.NavigateTo("/signup");
    }
}
