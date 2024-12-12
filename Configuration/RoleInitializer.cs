using Forms.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Forms.Configuration;

public static class RoleInitializer
{
    private static (string, string) getAdminCredentials()
    {
        string? adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
        string? adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");
        if (string.IsNullOrEmpty(adminPassword) || string.IsNullOrEmpty(adminEmail))
        {
            throw new InvalidOperationException(
                "ADMIN_EMAIL or ADMIN_PASSWORD environment variable is not set."
            );
        }
        return (adminEmail, adminPassword);
    }

    private static async Task ensureAdminRoleExists(RoleManager<IdentityRole> roleManager)
    {
        if (await roleManager.FindByNameAsync(Constants.AdminRoleName) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(Constants.AdminRoleName));
        }
    }

    private static async Task ensureAdminExists(UserManager<User> userManager)
    {
        const string Name = "ADMIN";
        (string email, string password) = getAdminCredentials();
        if (await userManager.FindByEmailAsync(email) == null)
        {
            User admin = new User { Email = email, UserName = Name };
            IdentityResult result = await userManager.CreateAsync(admin, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, Constants.AdminRoleName);
            }
        }
    }

    public static async Task InitializeAsync(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager
    )
    {
        await ensureAdminRoleExists(roleManager);
        await ensureAdminExists(userManager);
    }
}
