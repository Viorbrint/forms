using Forms.Data.Entities;
using Forms.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Forms.Components.Pages.UserPage;

// TODO: show snackbars

public partial class UserTemplates : ComponentBase
{
    private IEnumerable<Template> Templates { get; set; } = [];

    private HashSet<Template> SelectedTemplates { get; set; } = [];

    [Inject]
    private TemplateService TemplateService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private ICurrentUserService CurrentUserService { get; set; } = null!;

    private string UserId { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        UserId = CurrentUserService.UserId!;
        await ReloadTemplates();
    }

    private async Task ReloadTemplates()
    {
        Templates = await TemplateService.GetByUserAsync(UserId);
    }

    private async Task DeleteSelected()
    {
        var ids = SelectedTemplates.Select(x => x.Id).ToList();
        await TemplateService.DeleteByIdsAsync(ids);

        await ReloadTemplates();
    }

    // TODO: fix errors on spam
    private async Task PublishSelected()
    {
        var ids = SelectedTemplates.Select(x => x.Id).ToList();
        await TemplateService.PublishByIdsAsync(ids);

        await ReloadTemplates();
    }

    private async Task HideSelected()
    {
        var ids = SelectedTemplates.Select(x => x.Id).ToList();
        await TemplateService.HideByIdsAsync(ids);

        await ReloadTemplates();
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
        Template template = new() { AuthorId = UserId, TopicId = DEFAULT_TOPIC_ID };
        await TemplateService.AddAsync(template);

        NavigateToEdit(template.Id);
    }
}
