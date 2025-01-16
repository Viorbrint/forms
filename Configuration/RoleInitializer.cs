using Forms.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Forms.Configuration;

public static class RoleInitializer
{
    private static string AdminRoleName { get; } = "admin";

    private static (string, string) GetAdminCredentials()
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

    private static async Task EnsureAdminRoleExists(RoleManager<IdentityRole> roleManager)
    {
        if (await roleManager.FindByNameAsync(AdminRoleName) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(AdminRoleName));
        }
    }

    private static async Task EnsureAdminExists(UserManager<User> userManager)
    {
        const string Name = "ADMIN";
        (string email, string password) = GetAdminCredentials();
        if (await userManager.FindByEmailAsync(email) == null)
        {
            User admin = new() { Email = email, UserName = Name };
            IdentityResult result = await userManager.CreateAsync(admin, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, AdminRoleName);
            }
        }
    }

    public static async Task InitializeAsync(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager
    )
    {
        await EnsureAdminRoleExists(roleManager);
        await EnsureAdminExists(userManager);
    }
}
