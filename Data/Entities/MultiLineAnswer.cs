namespace Forms.Data.Entities;

public class MultiLineAnswer : Answer
{
    public string AnswerText { get; set; } = string.Empty;
    public override QuestionType Type { get; set; } = QuestionType.MultiLine;
}
