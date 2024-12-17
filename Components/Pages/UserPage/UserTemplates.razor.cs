using forms.Data.Entities;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.UserPage;

public partial class UserTemplates : ComponentBase
{
    private List<Template> Templates { get; set; } = [];
    
    private HashSet<Template> SelectedTemplates { get; set; } = [];

    private async Task DeleteTemplate()
    {
        throw new NotImplementedException();
    }
    
    private async Task EditTemplate()
    {
        throw new NotImplementedException();
    }
    
    private async Task CreateTemplate()
    {
        throw new NotImplementedException();
    }
}