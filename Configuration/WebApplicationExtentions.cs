using Forms.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Forms.Configuration;

public static class WebApplicationExtentions
{
    public static WebApplication UseAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<CookieLoginMiddleware>();
        return app;
    }

    public static async Task<WebApplication> InitializeAdminRole(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<User>>();
            var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await RoleInitializer.InitializeAsync(userManager, rolesManager);
        }
        return app;
    }

    public static WebApplication UseLocalization(this WebApplication app)
    {
        app.UseRequestLocalization(options =>
        {
            var supportedCultures = new[] { "en-US", "ru-RU" };
            options
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
        });
        return app;
    }
}
