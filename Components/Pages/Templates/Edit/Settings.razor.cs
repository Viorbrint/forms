using Forms.Data.Entities;
using Forms.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace Forms.Components.Pages.Templates.Edit;

public partial class Settings : ComponentBase
{
    [Inject]
    private TopicService TopicService { get; set; } = null!;

    [Inject]
    private TemplateSettings TemplateSettings { get; set; } = null!;

    [Parameter]
    public string TemplateId { get; set; } = null!;

    private List<string> Topics = [];

    private string TagInput = string.Empty;

    private IBrowserFile? Image { get; set; }

    private MudFileUpload<IBrowserFile>? _fileUpload;

    private string UserSearchInput = string.Empty;

    private List<IBrowserFile> UploadedFiles = [];

    private Task ClearAsync() => _fileUpload?.ClearAsync() ?? Task.CompletedTask;

    private void OnFileChanged(InputFileChangeEventArgs e) { }

    protected override async Task OnInitializedAsync()
    {
        Topics = await TopicService.GetAllNamesAsync();
        TemplateSettings.Initialize(TemplateId);
        await TemplateSettings.Load();
    }

    void RemoveTag(string tag)
    {
        TemplateSettings.Tags.Remove(tag);
    }

    private void AddTag()
    {
        if (string.IsNullOrWhiteSpace(TagInput))
        {
            return;
        }
        TemplateSettings.Tags.Add(TagInput);
        TagInput = string.Empty;
    }

    private void FilterUsers() { }

    private void RemoveUser(User user)
    {
        TemplateSettings.UsersWithAccess.Remove(user);
    }

    private async Task Save()
    {
        await TemplateSettings.Save();
    }
}
