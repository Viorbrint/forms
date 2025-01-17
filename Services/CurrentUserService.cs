using System.Security.Claims;
using Forms.Data.Entities;

namespace Forms.Services;

public class CurrentUserService(
    IHttpContextAccessor httpContextAccessor,
    UserService userService,
    SalesforceService salesforceService
) : ICurrentUserService
{
    public string? UserId => CurrentUser?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    public string? UserName => CurrentUser?.Identity?.Name;
    public bool UserIsAdmin => CurrentUser?.IsInRole("admin") ?? false;
    private ClaimsPrincipal? CurrentUser => httpContextAccessor.HttpContext?.User;
    private bool UserIsAuthenticated => CurrentUser?.Identity?.IsAuthenticated ?? false;

    public bool CurrentUserCanFill(Template template) =>
        template.IsPublished
        && UserIsAuthenticated
        && (
            UserIsAdmin
            || template.IsPublic
            || template.AuthorId == UserId
            || UserInWhitelist(template)
        );

    public async Task<bool> UserIsSyncedWithSalesforce()
    {
        return await userService.IsSyncedWithSalesforceById(UserId!);
    }

    public async Task SyncWithSalesforce(
        string accountName,
        string contactFirstName,
        string contactLastName,
        string contactEmail
    )
    {
        await salesforceService.CreateAccountWithContact(
            accountName,
            contactFirstName,
            contactLastName,
            contactEmail
        );
        await userService.SyncWithSalesforce(UserId);
    }

    public bool CurrentUserCanEditTemplate(Template template) =>
        UserIsAdmin || template.AuthorId == UserId;

    public bool CurrentUserCanEditForm(Form form) => UserIsAdmin || form.UserId == UserId;

    public bool CurrentUserCanViewForm(Form form) =>
        CurrentUserCanEditForm(form) || form.Template.AuthorId == UserId;

    public bool IsCurrentUserLikesTemplate(Template template) =>
        template.Likes.Any(l => l.UserId == UserId);

    private bool UserInWhitelist(Template template) =>
        template.TemplateAccesses.Any(x => x.UserId == UserId);
}
