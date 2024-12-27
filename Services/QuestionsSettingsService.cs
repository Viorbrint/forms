using Forms.Data.Entities;
using Forms.Models.Template;

namespace Forms.Services;

public class QuestionsSettingsService(TemplateService templateService)
{
    private string? TemplateId { get; set; }

    public List<QuestionSettingsModel> Questions { get; set; } = [];

    public void Initialize(string templateId) => TemplateId = templateId;

    public void DeleteQuestion(QuestionSettingsModel question)
    {
        Questions.Remove(question);
    }

    public void AddQuestion()
    {
        Questions.Add(new());
    }

    public async Task Save()
    {
        CheckInit();
        var template = await GetTemplateAsync();
        template.Questions = Questions
            .Select(
                (x, index) =>
                    new Question
                    {
                        QuestionText = x.Text,
                        Type = x.Type,
                        Order = index,
                        Template = template,
                        // TODO: IsVisibleInResults
                    }
            )
            .ToList();
        await templateService.UpdateAsync(template);
    }

    public async Task Load()
    {
        CheckInit();
        var template = await GetTemplateAsync();
        Questions = template
            .Questions.OrderBy(x => x.Order)
            .Select(x => new QuestionSettingsModel { Text = x.QuestionText, Type = x.Type })
            .ToList();
    }

    private async Task<Template> GetTemplateAsync()
    {
        var template = await templateService.GetByIdAsync(TemplateId!);
        if (template == null)
        {
            throw new ArgumentException("Template not found");
        }
        return template;
    }

    private void CheckInit()
    {
        if (TemplateId == null)
        {
            throw new ArgumentNullException("Id");
        }
    }
}
