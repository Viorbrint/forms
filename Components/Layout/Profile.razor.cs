using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Forms.Components.Layout;

public partial class Profile : ComponentBase
{
    [Inject]
    IHttpContextAccessor HttpContextAccessor { get; set; }
    
    [Inject]
    NavigationManager NavigationManager { get; set; } = null!;

    private string? _letter;
    private void Logout()
    {
        NavigationManager.NavigateTo("/logout", forceLoad: true);
    }
    
      protected override async Task OnInitializedAsync()
      {
          var context = HttpContextAccessor.HttpContext;
          if (context != null)
          {
              _letter = HttpContextAccessor.HttpContext.User.Identity.Name[0].ToString();
          }
      }
}