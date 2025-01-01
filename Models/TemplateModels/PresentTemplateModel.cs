namespace Forms.Models.TemplateModels;

public class PresentTemplateModel
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public int FilledFormsCount { get; set; }
}
