using Forms.Data.Entities;

namespace Forms.Models.Template;

public class QuestionModel
{
    public string Text { get; set; } = string.Empty;
    public QuestionType Type { get; set; }
}
