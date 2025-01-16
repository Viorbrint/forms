using Forms.Components;
using Forms.Data.Entities;
using Forms.Middleware;
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

    private static readonly string[] supportedCultures = ["en-US", "ru-RU"];

    public static WebApplication UseLocalization(this WebApplication app)
    {
        app.UseRequestLocalization(options =>
        {
            options
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
        });
        return app;
    }

    public static WebApplication UseDev(this WebApplication app)
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        app.UseHsts();
        return app;
    }

    public static async Task<WebApplication> UseAll(this WebApplication app)
    {
        await app.InitializeAdminRole();
        app.UseLocalization();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseAntiforgery();
        app.UseAuth();
        app.UseMiddleware<CultureMiddleware>();
        app.UseStatusCodePagesWithRedirects("/notfound");
        app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
        return app;
    }
}
