namespace Forms.Data.Entities;

public class QuestionType
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string QuestionTypeName { get; set; } = null!;

    public List<Question> Questions { get; set; } = [];
}
