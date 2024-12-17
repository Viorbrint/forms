using forms.Data.Entities;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.UserPage;

public partial class UserForms : ComponentBase
{
    private List<Form> Forms { get; set; } = [];
    
    private HashSet<Form> SelectedForms { get; set; } = [];

    private async Task DeleteForms()
    {
        throw new NotImplementedException();
    }
    
    private async Task EditForm()
    {
        throw new NotImplementedException();
    }
}