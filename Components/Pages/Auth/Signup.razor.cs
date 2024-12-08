using Forms.Services;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.Auth;

public partial class Signup : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private IAuthService AuthService { get; set; } = null!;

    public SignupModel signupModel = new();

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
            }
            else
            {
                // TODO: Handle error
                System.Console.WriteLine("Error signing up");
                System.Console.WriteLine(result.Errors.FirstOrDefault().Description);
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
