using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Forms.Components.Layout;

public partial class Profile : ComponentBase
{
    [Inject] private IHttpContextAccessor HttpContextAccessor { get; set; } = null!;
    
    [Inject]
    NavigationManager NavigationManager { get; set; } = null!;

    private string? _letter;
    private void Logout()
    {
        NavigationManager.NavigateTo("/logout", forceLoad: true);
    }
    
      protected override void OnInitialized()
      {
          var context = HttpContextAccessor.HttpContext;
          if (context is { User.Identity.Name: not null })
          {
              _letter = context.User.Identity.Name[0].ToString();
          }
      }
}