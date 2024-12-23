using Forms.Services;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.Templates.Edit;

public partial class TemplatesEdit : ComponentBase
{
    [Parameter]
    public string TemplateId { get; set; } = null!;

    [Inject]
    private ICurrentUserService CurrentUserService { get; set; } = null!;

    [Inject]
    private TemplateService TemplateService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var template = await TemplateService.GetByIdAsync(TemplateId);
        if (template == null)
        {
            NavigationManager.NavigateTo("/notfound");
            return;
        }

        var userId = CurrentUserService.GetUserId();
        var isAdmin = CurrentUserService.IsAdmin();
        if (!isAdmin && template.AuthorId != userId)
        {
            NavigationManager.NavigateTo("/accessdenied");
            return;
        }
    }
}
