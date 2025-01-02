using System.Text.RegularExpressions;
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
    private TemplateSettingsService TemplateSettingsService { get; set; } = null!;

    [Inject]
    private TagService TagService { get; set; } = null!;

    [Inject]
    private UserService UserService { get; set; } = null!;

    [Inject]
    private IImageService ImageService { get; set; } = null!;

    [Parameter]
    public string TemplateId { get; set; } = null!;

    private IEnumerable<string> Topics = [];

    private string TagInput { get; set; } = string.Empty;

    private IBrowserFile? Image { get; set; }

    private MudFileUpload<IBrowserFile>? FileUpload { get; set; }

    private User? _userInput;
    private User? UserInput
    {
        get => _userInput;
        set
        {
            TemplateSettingsService.settings.UsersWithAccess.Add(value);
            _userInput = null;
        }
    }

    private List<IBrowserFile> UploadedFiles = [];

    private Task ClearAsync() => FileUpload?.ClearAsync() ?? Task.CompletedTask;

    private void OnFileChanged(InputFileChangeEventArgs e) { }

    private bool IsLoading { get; set; } = false;

    private bool IsValid { get; set; } = true;

    private HashSet<User> SelectedUsers = [];

    // TODO: add validation to Tags , ...

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        Topics = await TopicService.GetAllNamesAsync();
        TemplateSettingsService.Initialize(TemplateId);
        await TemplateSettingsService.Load();
        IsLoading = false;
    }

    private async Task<IEnumerable<string>> SearchTags(string value, CancellationToken _)
    {
        return await TagService.SearchTagNames(value);
    }

    private async Task<IEnumerable<User>> SearchUsers(string value, CancellationToken _)
    {
        return await UserService.SearchUsers(value);
    }

    void RemoveTag(string tag)
    {
        TemplateSettingsService.settings.Tags.Remove(tag);
    }

    private void AddTag()
    {
        if (string.IsNullOrWhiteSpace(TagInput))
        {
            return;
        }
        TemplateSettingsService.settings.Tags.Add(TagInput);
        TagInput = string.Empty;
    }

    private void RemoveAccessFromSelectedUsers()
    {
        TemplateSettingsService.settings.UsersWithAccess.RemoveAll(u =>
            SelectedUsers.Select(u => u.Id).Contains(u.Id)
        );
    }

    private void RemoveUser(User user)
    {
        TemplateSettingsService.settings.UsersWithAccess.Remove(user);
    }

    private async Task Save()
    {
        await UploadImage();
        await TemplateSettingsService.Save();
    }

    private async Task UploadImage()
    {
        if (Image == null)
        {
            return;
        }
        const long MAX_SIZE_IN_MB = 10;
        const long MB_TO_BYTES_MULTIPLIER = 1024 * 1024;
        const long maxAllowedSize = MAX_SIZE_IN_MB * MB_TO_BYTES_MULTIPLIER;
        var stream = Image.OpenReadStream(maxAllowedSize);
        var extension = GetFileExtensionFromName(Image.Name);
        var ImageName =
            $"{TemplateSettingsService.settings.Title}-{DateTime.UtcNow:yyyyMMddHHmmss}{extension}";
        await ImageService.UploadFileAsync(stream, ImageName);
        var url = ImageService.GetPublicUrl(ImageName);
        TemplateSettingsService.settings.ImageUrl = url;
    }

    private string GetFileExtensionFromName(string fileName)
    {
        var match = MyRegex().Match(fileName);
        return match.Success ? $".{match.Groups[1].Value}" : string.Empty;
    }

    private async Task Publish()
    {
        await TemplateSettingsService.Publish();
        await TemplateSettingsService.Load();
    }

    private async Task Hide()
    {
        await TemplateSettingsService.Hide();
        await TemplateSettingsService.Load();
    }

    [GeneratedRegex(@"\.(\w+)$")]
    private static partial Regex MyRegex();
}
