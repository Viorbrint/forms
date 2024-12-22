using Microsoft.AspNetCore.Identity;

namespace Forms.Data.Entities;

public class User : IdentityUser
{
    public List<Template> Templates { get; set; } = [];

    public List<Form> Forms { get; set; } = [];

    public List<TemplateLike> TemplateLikes { get; set; } = [];

    public List<TemplateAccess> TemplateAccesses { get; set; } = [];

    public List<Comment> Comments { get; set; } = [];
}
