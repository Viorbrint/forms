using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.Templates.Edit;

public partial class TemplatesEdit : ComponentBase
{
    [Parameter]
    public string? TemplateId { get; set; }
}
