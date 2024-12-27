using Forms.Data.Entities;

namespace Forms.Models.Template;

public class TemplateModel
{
    public string ImageUrl { get; set; } = string.Empty;
    public int Likes { get; set; }
    public List<Comment> Comments { get; set; } = [];
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = [];
    public List<QuestionModel> Questions { get; set; } = [];
}
