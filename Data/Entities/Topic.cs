namespace forms.Data.Entities;

public class Topic
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    // unique
    public string TopicName { get; set; } = string.Empty;

    public List<Template> Templates { get; set; } = [];
}
