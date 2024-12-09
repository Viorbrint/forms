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

    [Inject]
    private ApiClient client { get; set; } = null!;

    public SigninModel signinModel = new();

    private async Task HandleValidSubmit()
    {
        var response = await client.Signin(signinModel);

        if (response.IsSuccessStatusCode)
        {
            // maybe to user page
            NavigateToMain();
        }
        else
        {
            var details = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            if (details != null)
            {
                // TODO: show to user
                System.Console.WriteLine(details.Detail);
            }
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
