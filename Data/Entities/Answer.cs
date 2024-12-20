namespace forms.Data.Entities;

public abstract class Answer
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string FormId { get; set; } = null!;
    public Form Form { get; set; } = null!;

    public string QuestionId { get; set; } = null!;
    public Question Question { get; set; } = null!;
}
