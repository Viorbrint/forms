using Forms.Data.Entities;
using Forms.Services;

public class TemplateSettings(TemplateService templateService, TopicService topicService)
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = [];
    public bool IsPublic { get; set; } = true;
    public List<User> UsersWithAccess { get; set; } = [];

    private string? Id { get; set; }

    // TODO: image

    public void Initialize(string templateId) => Id = templateId;

    private void CheckInit()
    {
        if (Id == null)
        {
            throw new ArgumentNullException("Id");
        }
    }

    public async Task Save()
    {
        CheckInit();

        var template = await templateService.GetByIdAsync(Id!);
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
        // TODO: tags...

        await templateService.UpdateAsync(template);
    }

    public async Task Load()
    {
        CheckInit();

        var template = await templateService.GetByIdAsync(Id!);

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
}
