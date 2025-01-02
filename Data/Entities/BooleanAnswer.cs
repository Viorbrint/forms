namespace Forms.Data.Entities;

public class BooleanAnswer : Answer
{
    public bool AnswerBoolean { get; set; }
    public override QuestionType Type { get; set; } = QuestionType.Boolean;
}
