using Forms.Data.Entities;

namespace Forms.Models.Template;

public class QuestionSettingsModel
{
    public string Text { get; set; } = string.Empty;

    public QuestionType Type { get; set; }

    // TODO:
    // public bool IsVisibleInResults { get; set; }
}
