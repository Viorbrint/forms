using Forms.Data.Entities;
using Forms.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace Forms.Components.Pages.Auth;

public partial class Signin : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IAuthService AuthService { get; set; } = null!;

    [Inject]
    private UserManager<User> UserManager { get; set; } = null!;

    public SigninModel signinModel = new();

    private async Task HandleValidSubmit()
    {
        var user = await UserManager.FindByEmailAsync(signinModel.Email);
        if (user == null)
        {
            // TODO: Handle error
            return;
        }
        var checkResult = await AuthService.CheckSignin(user, signinModel.Password);
        if (checkResult.Succeeded)
        {
            var key = Guid.NewGuid();
            CookieLoginMiddleware.Logins.Add(key, (user, signinModel.Password));
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
