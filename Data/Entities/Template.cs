using Forms.Data.Entities;

namespace forms.Data.Entities;

public class Template
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string? ImageUrl { get; set; }

    public bool IsPublic { get; set; }

    public bool IsPublished { get; set; }

    public string AuthorId { get; set; } = null!;

    public User Author { get; set; } = null!;

    public string TopicId { get; set; } = null!;

    public Topic Topic { get; set; } = null!;

    public List<Form> Forms { get; set; } = [];

    public List<Tag> Tags { get; set; } = [];

    public List<TemplateLike> Likes { get; set; } = [];

    public List<Question> Questions { get; set; } = [];

    public List<Comment> Comments { get; set; } = [];

    public List<TemplateAccess> TemplateAccesses { get; set; } = [];
}