namespace forms.Data.Entities;

public class Question
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string QuestionText { get; set; } = null!;

    public bool IsVisibleInResults { get; set; } = true;

    public string QuestionTypeId { get; set; } = null!;
    public QuestionType QuestionType { get; set; } = null!;

    public string TemplateId { get; set; } = null!;
    public Template Template { get; set; } = null!;

    public List<TextAnswer> TextAnswers { get; set; } = [];
    public List<NumberAnswer> NumberAnswers { get; set; } = [];
    public List<BooleanAnswer> BooleanAnswers { get; set; } = [];
}
