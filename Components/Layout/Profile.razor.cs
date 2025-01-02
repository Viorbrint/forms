using Forms.Services;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Layout;

public partial class Profile : ComponentBase
{
    [Inject]
    private ICurrentUserService CurrentUserService { get; set; } = null!;

    [Inject]
    NavigationManager NavigationManager { get; set; } = null!;

    private string? _letter;

    private void Logout()
    {
        NavigationManager.NavigateTo("/logout", forceLoad: true);
    }

    protected override void OnInitialized()
    {
        var userName = CurrentUserService.UserName!;
        if (userName != null)
        {
            _letter = userName[0].ToString();
        }
        else
        {
            const string GUESS_LETTER = "U";
            _letter = GUESS_LETTER;
        }
    }
}
