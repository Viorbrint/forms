namespace Forms.Data.Entities;

public class TemplateAccess
{
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public string TemplateId { get; set; } = null!;
    public Template Template { get; set; } = null!;
}
