using Forms.Models.Auth;
using Forms.Services;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.Auth.Signup;

public partial class Signup : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private SnackbarFacade Snackbar { get; set; } = null!;

    [Inject]
    private IAuthService AuthService { get; set; } = null!;

    public SignupModel signupModel = new();

    private string ErrorMessage { get; set; } = "";

    private bool IsProcessing { get; set; } = false;

    private async Task HandleValidSubmit()
    {
        if (IsProcessing)
        {
            return;
        }

        IsProcessing = true;

        try
        {
            var result = await AuthService.Signup(
                signupModel.Username,
                signupModel.Email,
                signupModel.Password
            );
            if (result.Succeeded)
            {
                NavigateToLogin();
                Snackbar.Success("Signup successful!");
            }
            else
            {
                ErrorMessage = result.Errors.FirstOrDefault()?.Description ?? "";
                Snackbar.Error("Signup error!");
            }
        }
        finally
        {
            IsProcessing = false;
        }
    }

    private void NavigateToLogin()
    {
        NavigationManager.NavigateTo("/signin");
    }
}
