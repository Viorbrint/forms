using Forms.Data;
using Forms.Data.Entities;
using Forms.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forms.Configuration;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, IdentityAuthService>();
        var apiBaseUrl = Environment.GetEnvironmentVariable("API_BASE_URL");
        if (string.IsNullOrEmpty(apiBaseUrl))
        {
            throw new InvalidOperationException("API_BASE_URL environment variable is not set.");
        }
        services.AddHttpClient<ApiClient>(client =>
        {
            client.BaseAddress = new Uri(apiBaseUrl);
        });
        return services;
    }

    public static IServiceCollection AddBlazor(this IServiceCollection services)
    {
        services.AddRazorComponents().AddInteractiveServerComponents();
        return services;
    }

    public static IServiceCollection ConfigureDbContext(this IServiceCollection services)
    {
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        if (string.IsNullOrEmpty(databaseUrl))
        {
            throw new InvalidOperationException("DATABASE_URL environment variable is not set.");
        }
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(databaseUrl));
        return services;
    }

    public static IServiceCollection AddIdentityConfig(this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.Lockout.AllowedForNewUsers = true;

                options.SignIn.RequireConfirmedAccount = false;

                options.User.RequireUniqueEmail = true;
            })
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        return services;
    }

    public static IServiceCollection AddCookieConfig(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromHours(1);
            options.SlidingExpiration = true;
        });
        return services;
    }

    public static IServiceCollection AddCorsConfig(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(
                "AllowAll",
                policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                }
            );
        });
        return services;
    }

    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }
}