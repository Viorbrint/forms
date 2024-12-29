namespace Forms.Data.Entities;

public class NumberAnswer : Answer
{
    public int AnswerNumber { get; set; }
    public override QuestionType Type { get; set; } = QuestionType.Number;
}
