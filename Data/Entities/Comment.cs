namespace Forms.Data.Entities;

public class Comment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string TemplateId { get; set; } = null!;
    public Template Template { get; set; } = null!;

    public string AuthorId { get; set; } = null!;
    public User Author { get; set; } = null!;
}
