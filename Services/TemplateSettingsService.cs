using Forms.Models.Template;

namespace Forms.Services;

public class TemplateSettingsService(
    TemplateService templateService,
    TopicService topicService,
    TagService tagService
)
{
    public TemplateSettingsModel settings = new();

    private string? TemplateId { get; set; }

    // TODO: image

    public void Initialize(string templateId) => TemplateId = templateId;

    public async Task Save()
    {
        // TODO: refactor this
        CheckInit();
        var template = await templateService.GetByIdAsync(TemplateId!);
        if (template == null)
        {
            // TODO: do smth
            return;
        }
        template.Title = settings.Title;
        template.Description = settings.Description;
        var dbTopic = await topicService.GetByNameAsync(settings.Topic);
        if (dbTopic == null)
        {
            throw new ArgumentException("Topic does not exist in database");
        }
        template.Topic = dbTopic;
        await tagService.AddRangeAsync(settings.Tags);
        var dbTags = await tagService.GetTagsByNamesAsync(settings.Tags);
        template.Tags = dbTags.ToList();
        await templateService.UpdateAsync(template);
    }

    public async Task Load()
    {
        CheckInit();

        var template = await templateService.GetByIdAsync(TemplateId!);

        if (template == null)
        {
            // TODO:
            return;
        }

        settings.Title = template.Title;
        settings.Description = template.Description;
        settings.Topic = template.Topic.TopicName;
        settings.Tags = template.Tags.Select(t => t.TagName).ToList();
        settings.IsPublic = template.IsPublic;
        settings.UsersWithAccess = template.TemplateAccesses.Select(x => x.User).ToList();
    }

    private void CheckInit()
    {
        if (TemplateId == null)
        {
            throw new ArgumentNullException("Id");
        }
    }
}
