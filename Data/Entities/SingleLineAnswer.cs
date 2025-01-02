namespace Forms.Data.Entities;

public class SingleLineAnswer : Answer
{
    public string AnswerText { get; set; } = string.Empty;
    public override QuestionType Type { get; set; } = QuestionType.SingleLine;
}
