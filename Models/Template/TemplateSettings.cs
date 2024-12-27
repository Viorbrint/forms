using Forms.Data.Entities;
using Forms.Services;

public class TemplateSettings(
    TemplateService templateService,
    TopicService topicService,
    TagService tagService
)
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = [];
    public bool IsPublic { get; set; } = true;
    public List<User> UsersWithAccess { get; set; } = [];

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
        template.Title = Title;
        template.Description = Description;
        var dbTopic = await topicService.GetByNameAsync(Topic);
        if (dbTopic == null)
        {
            throw new ArgumentException("Topic does not exist in database");
        }
        template.Topic = dbTopic;
        await tagService.AddRangeAsync(Tags);
        var dbTags = await tagService.GetTagsByNamesAsync(Tags);
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

        Title = template.Title;
        Description = template.Description;
        Topic = template.Topic.TopicName;
        Tags = template.Tags.Select(t => t.TagName).ToList();
        IsPublic = template.IsPublic;
        UsersWithAccess = template.TemplateAccesses.Select(x => x.User).ToList();
    }

    private void CheckInit()
    {
        if (TemplateId == null)
        {
            throw new ArgumentNullException("Id");
        }
    }
}
