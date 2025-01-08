using Microsoft.AspNetCore.Localization;

namespace Forms.Middleware;

public class CultureMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        if (
            context.Request.Query.TryGetValue("culture", out var cultureValues)
            && !string.IsNullOrEmpty(cultureValues)
        )
        {
            var culture = cultureValues.ToString();
            var requestCulture = new RequestCulture(culture);
            string cookieName = CookieRequestCultureProvider.DefaultCookieName;
            string cookieValue = CookieRequestCultureProvider.MakeCookieValue(requestCulture);
            context.Response.Cookies.Append(cookieName, cookieValue);
        }
        await _next.Invoke(context);
    }
}
