using Forms.Services;

public class CookieLoginMiddleware
{
    public static Dictionary<Guid, SigninModel> Logins { get; } = [];

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
            var model = Logins[key];

            var result = await authService.Signin(model.Email, model.Password);
            if (result.Succeeded)
            {
                Logins.Remove(key);
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
