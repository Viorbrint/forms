using Forms.Data.Entities;

namespace Forms.Models.TemplateModels;

public class TemplateModel
{
    public string? ImageUrl { get; set; }
    public int Likes { get; set; }
    public List<CommentModel> Comments { get; set; } = [];
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = [];
    public List<QuestionModel> Questions { get; set; } = [];

    public static TemplateModel MapTemplate(Template template)
    {
        return new()
        {
            ImageUrl = template.ImageUrl,
            Likes = template.Likes.Count,
            Tags = template.Tags.Select(t => t.TagName).ToList(),
            Topic = template.Topic.TopicName,
            Description = template.Description,
            Title = template.Title,
            Questions = template.Questions.Select(q => QuestionModel.MapQuestion(q)).ToList(),
            Comments = template.Comments.Select(c => CommentModel.MapComment(c)).ToList(),
        };
    }
}
