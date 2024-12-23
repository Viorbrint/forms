using Forms.Data.Entities;
using Forms.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Forms.Components.Pages.UserPage;

public partial class UserTemplates : ComponentBase
{
    private List<Template> Templates { get; set; } = [];

    private HashSet<Template> SelectedTemplates { get; set; } = [];

    [Inject]
    private TemplateService TemplateService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private ICurrentUserService CurrentUserService { get; set; } = null!;

    private string userId = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        userId = CurrentUserService.GetUserId()!;
        Templates = await TemplateService.GetByUserAsync(userId);
    }

    private async Task DeleteTemplates()
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    private void OnTemplateClick(TableRowClickEventArgs<Template> args)
    {
        var template = args.Item;
        if (template != null)
        {
            NavigateToEdit(template.Id);
        }
    }

    private void NavigateToEdit(string templateId)
    {
        var editPath = $"/templates/edit/{templateId}";
        NavigationManager.NavigateTo(editPath);
    }

    private void EditSelected()
    {
        if (SelectedTemplates.Count() != 1)
        {
            return;
        }
        var template = SelectedTemplates.First();
        NavigateToEdit(template.Id);
    }

    private async Task CreateTemplate()
    {
        // TODO: move this
        const string DEFAULT_TOPIC_ID = "20";
        Template template = new() { AuthorId = userId, TopicId = DEFAULT_TOPIC_ID };
        await TemplateService.AddAsync(template);

        NavigateToEdit(template.Id);
    }
}
