using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Forms.Components.Pages.Search;

public partial class Search : ComponentBase
{
    [SupplyParameterFromQuery(Name = "query")]
    [Parameter]
    public string? InitialQuery { get; set; }

    private string _searchQuery = string.Empty;
    private string SearchQuery {get => _searchQuery; set {
        _searchQuery = value;
        PerformSearch();
    }}

    private List<Template> SearchResults = new();

    protected override void OnParametersSet()
    {
        SearchQuery = InitialQuery ?? string.Empty;
    }

    private void HandleSearchKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Enter")
        {
            PerformSearch();
        }
    }

    private void PerformSearch()
    {
        // Simulate search logic
        SearchResults = AllTemplates
            .Where(t => t.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                        t.Description.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                        t.Tags.Any(tag => tag.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }

    private List<Template> AllTemplates = new()
    {
        new Template { Id = 1, Name = "Template 1", Description = "A template about education", ImageUrl = "https://via.placeholder.com/150", Tags = new() { "Education", "Quiz" } },
        new Template { Id = 2, Name = "Template 2", Description = "A template about surveys", ImageUrl = "https://via.placeholder.com/150", Tags = new() { "Survey", "Feedback" } },
        new Template { Id = 3, Name = "Template 3", Description = "A fun template for polls", ImageUrl = "https://via.placeholder.com/150", Tags = new() { "Poll", "Fun" } },
    };

    private class Template
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = new();
    }
}