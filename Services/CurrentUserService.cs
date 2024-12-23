using System.Security.Claims;

namespace Forms.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public ClaimsPrincipal? GetCurrentUser()
    {
        return httpContextAccessor.HttpContext?.User;
    }

    public string? GetUserId()
    {
        return httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public string? GetUserName()
    {
        return httpContextAccessor.HttpContext?.User?.Identity?.Name;
    }

    public string? GetUserEmail()
    {
        return httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
    }

    public bool IsAuthenticated()
    {
        return httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
