namespace forms.Data.Entities;

public class Tag
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    // unique
    public string TagName { get; set; } = string.Empty;
}
