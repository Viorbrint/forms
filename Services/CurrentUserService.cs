using System.Security.Claims;
using Forms.Data.Entities;

namespace Forms.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private ClaimsPrincipal? CurrentUser => httpContextAccessor.HttpContext?.User;

    public string? UserId =>
        httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public string? UserName => httpContextAccessor.HttpContext?.User?.Identity?.Name;

    private string? UserEmail =>
        httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

    private bool UserIsAuthenticated =>
        httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    private bool UserIsAdmin =>
        httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value == "admin";

    public bool CurrentUserCanFill(Template template)
    {
        if (!template.IsPublished)
        {
            return false;
        }
        if (
            UserIsAdmin
            || template.IsPublic
            || template.AuthorId == UserId
            || UserInWhitelist(template)
        )
        {
            return true;
        }
        return false;
    }

    public bool CurrentUserCanEditTemplate(Template template)
    {
        if (!UserIsAdmin && template.AuthorId != UserId)
        {
            return false;
        }
        return true;
    }

    public bool CurrentUserCanEditForm(Form form)
    {
        if (!UserIsAdmin && form.UserId != UserId)
        {
            return false;
        }
        return true;
    }

    private bool UserInWhitelist(Template template)
    {
        var userIdsWithAccess = template.TemplateAccesses.Select(x => x.UserId);
        if (userIdsWithAccess.Contains(UserId))
        {
            return true;
        }
        return false;
    }
}
