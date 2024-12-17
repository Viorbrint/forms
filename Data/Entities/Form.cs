using Forms.Data.Entities;

namespace forms.Data.Entities;

public class Form
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public string TemplateId { get; set; } = null!;
    public Template Template { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    public List<SingleLineAnswer> SingleLineAnswers { get; set; } = [];
    public List<MultiLineAnswer> MultiLineAnswers { get; set; } = [];
    public List<NumberAnswer> NumberAnswers { get; set; } = [];
    public List<BooleanAnswer> BooleanAnswers { get; set; } = [];
}
