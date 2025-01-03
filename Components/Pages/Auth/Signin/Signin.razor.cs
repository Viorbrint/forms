using Forms.Data.Entities;
using Forms.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace Forms.Components.Pages.Auth.Signin;

public partial class Signin : ComponentBase
{
    [Inject]
    private SnackbarFacade Snackbar { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IAuthService AuthService { get; set; } = null!;

    [Inject]
    private UserManager<User> UserManager { get; set; } = null!;

    public SigninModel signinModel = new();

    private string ErrorMessage { get; set; } = "";

    private async Task HandleValidSubmit()
    {
        var user = await UserManager.FindByEmailAsync(signinModel.Email);
        if (user == null)
        {
            ErrorMessage = "Incorrect email";
            return;
        }
        var checkResult = await AuthService.CheckSignin(user, signinModel.Password);
        if (checkResult.Succeeded)
        {
            var key = Guid.NewGuid();
            CookieLoginMiddleware.Logins.Add(key, (user, signinModel.Password));
            NavigationManager.NavigateTo($"/signin?key={key}", true);
            // TODO: somehow apply snackbar
        }
        else
        {
            Snackbar.Error("Signin error!");
            if (checkResult.IsLockedOut)
            {
                ErrorMessage =
                    "Your account has been blocked. If you think this is a mistake, please contact the administrator.";
            }
            else
            {
                ErrorMessage = "Incorrect password";
            }
        }
    }

    private void NavigateToSignup()
    {
        NavigationManager.NavigateTo("/signup");
    }
}
