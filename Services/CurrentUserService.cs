using System.Security.Claims;
using Forms.Data.Entities;

namespace Forms.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private ClaimsPrincipal? CurrentUser => httpContextAccessor.HttpContext?.User;
    public string? UserId => CurrentUser?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    public string? UserName => CurrentUser?.Identity?.Name;
    public bool UserIsAuthenticated => CurrentUser?.Identity?.IsAuthenticated ?? false;
    public bool UserIsAdmin => CurrentUser?.IsInRole("admin") ?? false;

    public bool CurrentUserCanFill(Template template) =>
        template.IsPublished
        && (
            UserIsAdmin
            || template.IsPublic
            || template.AuthorId == UserId
            || UserInWhitelist(template)
        );

    public bool CurrentUserCanEditTemplate(Template template) =>
        UserIsAdmin || template.AuthorId == UserId;

    public bool CurrentUserCanEditForm(Form form) => UserIsAdmin || form.UserId == UserId;

    public bool CurrentUserCanViewForm(Form form) =>
        CurrentUserCanEditForm(form) || form.Template.AuthorId == UserId;

    private bool UserInWhitelist(Template template) =>
        template.TemplateAccesses.Any(x => x.UserId == UserId);
}
