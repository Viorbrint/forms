using Forms.Data.Entities;

namespace Forms.Services;

public interface ICurrentUserService
{
    bool CurrentUserCanEdit(Template template);
    bool CurrentUserCanFill(Template template);
    string? UserId { get; }
    string? UserName { get; }
}
