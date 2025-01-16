using Forms.Data.Entities;

namespace Forms.Services;

public interface ICurrentUserService
{
    Task<bool> UserIsSyncedWithSalesforce();
    Task SyncWithSalesforce(
        string accountName,
        string contactFirstName,
        string contactLastName,
        string contactEmail
    );
    bool CurrentUserCanEditTemplate(Template template);
    bool CurrentUserCanFill(Template template);
    bool CurrentUserCanEditForm(Form form);
    bool CurrentUserCanViewForm(Form form);
    bool IsCurrentUserLikesTemplate(Template template);
    bool UserIsAdmin { get; }

    string? UserId { get; }
    string? UserName { get; }
}
