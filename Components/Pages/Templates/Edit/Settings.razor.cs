using Forms.Data;
using Forms.Data.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace Forms.Components.Pages.Templates.Edit;

public partial class Settings : ComponentBase
{
    private string Title = string.Empty;
    private string Description = string.Empty;
    private string SelectedTopic = string.Empty;
    private List<string> Topics = [];
    private List<string> SelectedTags = new();
    private List<string> FilteredTags = new();
    private string TagInput = string.Empty;

    private IBrowserFile? Image { get; set; }

    private MudFileUpload<IBrowserFile>? _fileUpload;

    private Task ClearAsync() => _fileUpload?.ClearAsync() ?? Task.CompletedTask;

    private void OnFileChanged(InputFileChangeEventArgs e) { }

    private bool IsPublic { get; set; } = true;
    private string UserSearchInput = string.Empty;
    private List<User> SelectedUsers = new();
    private List<User> FilteredUsers = new();

    [Inject]
    ApplicationDbContext Db { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var topics = await Db.Topics.ToListAsync();
        Topics = topics.Select(x => x.TopicName).ToList();
    }

    void RemoveTag(string tag)
    {
        SelectedTags.Remove(tag);
    }

    private void AddTag()
    {
        if (string.IsNullOrWhiteSpace(TagInput))
        {
            return;
        }
        SelectedTags.Add(TagInput);
        TagInput = string.Empty;
    }

    private void FilterUsers() { }

    private void RemoveUser(User user)
    {
        SelectedUsers.Remove(user);
    }

    private List<IBrowserFile> UploadedFiles = new();
}
