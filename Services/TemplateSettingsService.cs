using Forms.Data.Entities;
using Forms.Models.TemplateModels;

namespace Forms.Services;

public class TemplateSettingsService(
    TemplateService templateService,
    TopicService topicService,
    TagService tagService,
    UserService userService
)
{
    public TemplateSettingsModel settings = new();

    private string? TemplateId { get; set; }

    // TODO: image

    public void Initialize(string templateId) => TemplateId = templateId;

    public async Task Publish()
    {
        CheckInit();
        await templateService.PublishByIdAsync(TemplateId!);
    }

    public async Task Hide()
    {
        CheckInit();
        await templateService.HideByIdAsync(TemplateId!);
    }

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
        var dbTopic =
            await topicService.GetByNameAsync(settings.Topic)
            ?? throw new ArgumentException("Topic does not exist in database");
        template.Topic = dbTopic;
        await tagService.AddRangeAsync(settings.Tags);
        var dbTags = await tagService.GetTagsByNamesAsync(settings.Tags);
        template.Tags = dbTags.ToList();
        template.IsPublic = settings.IsPublic;
        template.TemplateAccesses = settings
            .UsersWithAccess.Select(u => new TemplateAccess() { Template = template, User = u })
            .ToList();
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
        settings.IsPublished = template.IsPublished;
        settings.UsersWithAccess = await userService.GetWithAccessToTemplate(template.Id);
    }

    private void CheckInit()
    {
        if (TemplateId == null)
        {
            throw new ArgumentNullException("Id");
        }
    }
}
