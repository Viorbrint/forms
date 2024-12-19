using forms.Data.Entities;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.UserPage;

public partial class UserTemplates : ComponentBase
{
    private List<Template> Templates { get; set; } = [];

    private HashSet<Template> SelectedTemplates { get; set; } = [];

    private async Task DeleteTemplate()
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private async Task EditTemplate()
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private async Task CreateTemplate()
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}

