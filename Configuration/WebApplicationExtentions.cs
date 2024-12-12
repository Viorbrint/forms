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
}
