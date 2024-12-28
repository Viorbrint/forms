using Forms.Models.TemplateModels;
using Forms.Services;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.Templates.Fill;

public partial class TemplatesFill : ComponentBase
{
    [Parameter]
    public string TemplateId { get; set; } = null!;

    [Inject]
    private ICurrentUserService CurrentUserService { get; set; } = null!;

    [Inject]
    private TemplateService TemplateService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    private TemplateModel TemplateModel = new();

    private bool IsLoading { get; set; } = false;

    private string UserId { get; set; } = null!;

    private bool IsUserLikeTemplate { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        var template = await TemplateService.GetByIdAsync(TemplateId);
        if (template == null)
        {
            NavigationManager.NavigateTo("/notfound");
            return;
        }

        UserId = CurrentUserService.UserId!;
        IsUserLikeTemplate = template.Likes.Any(l => l.UserId == UserId);

        var canFill = CurrentUserService.CurrentUserCanFill(template);
        if (!canFill)
        {
            // TODO: readonly mode
            NavigationManager.NavigateTo("/accessdenied");
            return;
        }

        TemplateModel = TemplateModel.MapTemplate(template);
        IsLoading = false;
    }

    private async Task ToggleLike()
    {
        await TemplateService.ToggleLike(UserId, TemplateId);
    }

    private async Task SubmitForm()
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
