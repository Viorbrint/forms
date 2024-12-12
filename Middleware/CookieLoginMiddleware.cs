using Forms.Data.Entities;
using Forms.Services;

public class CookieLoginMiddleware
{
    public static Dictionary<Guid, (User, string)> Logins { get; } = [];

    private readonly RequestDelegate _next;

    public CookieLoginMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IAuthService authService)
    {
        if (context.Request.Path == "/signin" && context.Request.Query.ContainsKey("key"))
        {
            var key = Guid.Parse(context.Request.Query["key"]);
            var (user, password) = Logins[key];

            var result = await authService.Signin(user, password);
            Logins.Remove(key);
            if (result.Succeeded)
            {
                context.Response.Redirect("/");
            }
            else
            {
                // TODO: Handle error
                throw new Exception("Login failed");
            }
        }
        else
        {
            await _next.Invoke(context);
        }
    }
}
