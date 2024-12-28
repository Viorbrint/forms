using Forms.Data.Entities;

namespace Forms.Models.TemplateModels;

public class QuestionModel
{
    public string Text { get; set; } = string.Empty;
    public QuestionType Type { get; set; }
    public string TextAnswer { get; set; } = string.Empty;
    public int NumberAnswer { get; set; }
    public bool BooleanAnswer { get; set; }

    public static QuestionModel MapQuestion(Question question)
    {
        return new() { Text = question.QuestionText, Type = question.Type };
    }
}
