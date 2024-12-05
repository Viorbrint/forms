using Forms.Data.Entities;

namespace forms.Data.Entities;

public class TemplateLike
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public string TemplateId { get; set; } = null!;
    public Template Template { get; set; } = null!;
}
