using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages;

public partial class Home : ComponentBase
{
    private List<Template> LatestTemplates = new()
    {
        new Template
        {
            Id = 1,
            Title = "Template 1",
            Description =
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
            ImageUrl = "https://via.placeholder.com/150",
        },
        new Template
        {
            Id = 2,
            Title = "Template 2",
            Description = "Description for Template 2",
            ImageUrl = "https://via.placeholder.com/150",
        },
        new Template
        {
            Id = 3,
            Title = "Template 3",
            Description = "Description for Template 3",
            ImageUrl = "https://via.placeholder.com/150",
        },
        new Template
        {
            Id = 4,
            Title = "Template 4",
            Description = "Description for Template 4",
            ImageUrl = "https://via.placeholder.com/150",
        },
    };

    private List<Template> TopPopularTemplates = new()
    {
        new Template
        {
            Id = 1,
            Title = "Popular Template 1",
            FilledFormsCount = 150,
        },
        new Template
        {
            Id = 2,
            Title = "Popular Template 2",
            FilledFormsCount = 120,
        },
        new Template
        {
            Id = 3,
            Title = "Popular Template 3",
            FilledFormsCount = 90,
        },
        new Template
        {
            Id = 4,
            Title = "Popular Template 4",
            FilledFormsCount = 85,
        },
        new Template
        {
            Id = 5,
            Title = "Popular Template 5",
            FilledFormsCount = 80,
        },
    };

    private List<string> Tags = new() { "Education", "Quiz", "Survey", "Feedback", "Poll" };

    private void NavigateToSearch(string tag)
    {
        NavigationManager.NavigateTo($"/search?tag={tag}");
    }

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    private class Template
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int FilledFormsCount { get; set; }
    }
}
