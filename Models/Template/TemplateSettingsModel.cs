using Forms.Data.Entities;

namespace Forms.Models.Template;

public class TemplateSettingsModel
{
    public string Description { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = [];
    public bool IsPublic { get; set; } = true;
    public List<User> UsersWithAccess { get; set; } = [];
}
