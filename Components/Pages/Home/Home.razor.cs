using Forms.Models.TemplateModels;
using Forms.Services;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.Home;

public partial class Home : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private TemplateService TemplateService { get; set; } = null!;

    [Inject]
    private TagService TagService { get; set; } = null!;

    private IEnumerable<PresentTemplateModel> LatestTemplates = [];
    private IEnumerable<PresentTemplateModel> PopularTemplates = [];
    private IEnumerable<string> PopularTags = [];

    private int NumberOfLatestTemplates { get; } = 4;
    private int NumberOfPopularTemplates { get; } = 5;
    private int NumberOfPopularTags { get; } = 5;

    private bool IsLoading { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        LatestTemplates = await TemplateService.GetLatestTemplatesAsync(NumberOfLatestTemplates);
        PopularTemplates = await TemplateService.GetPopularTemplatesAsync(NumberOfPopularTemplates);
        PopularTags = await TagService.GetPopularTagsAsync(NumberOfPopularTags);
        IsLoading = false;
    }

    private void NavigateToSearch(string tag)
    {
        NavigationManager.NavigateTo($"/search?tag={tag}");
    }
}
