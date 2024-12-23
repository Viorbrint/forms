using System.Security.Claims;

namespace Forms.Services;

public interface ICurrentUserService
{
    ClaimsPrincipal? GetCurrentUser();
    string? GetUserEmail();
    string? GetUserId();
    string? GetUserName();
    bool IsAuthenticated();
}
