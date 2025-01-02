namespace Forms.Data.Entities;

public class Question
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string QuestionText { get; set; } = null!;

    public bool IsVisibleInResults { get; set; } = true;

    public QuestionType Type { get; set; }

    public string TemplateId { get; set; } = null!;
    public Template Template { get; set; } = null!;

    public int Order { get; set; }

    public List<Answer> Answers { get; set; } = [];
}
