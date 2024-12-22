namespace Forms.Data.Entities;

public class TemplateLike
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public string TemplateId { get; set; } = null!;
    public Template Template { get; set; } = null!;
}
