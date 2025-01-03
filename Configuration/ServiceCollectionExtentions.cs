using Blazored.LocalStorage;
using Forms.Data;
using Forms.Data.Entities;
using Forms.Repositories;
using Forms.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

namespace Forms.Configuration;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddImageService(this IServiceCollection services)
    {
        var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
        var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");
        var bucketName = Environment.GetEnvironmentVariable("SUPABASE_BUCKET_NAME");
        if (
            string.IsNullOrEmpty(url)
            || string.IsNullOrEmpty(key)
            || string.IsNullOrEmpty(bucketName)
        )
        {
            throw new InvalidOperationException(
                "SUPABASE_URL or/and SUPABASE_KEY or/and SUPABASE_BUCKET_NAME environment variable is not set."
            );
        }
        services.AddSingleton<IImageService>(sp => new SupabaseImageService(url, key, bucketName));
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, IdentityAuthService>();
        services.AddScoped<IRepository<Template>, Repository<Template>>();
        services.AddScoped<TemplateService>();
        services.AddScoped<IRepository<Form>, Repository<Form>>();
        services.AddScoped<UserService>();
        services.AddScoped<IRepository<User>, Repository<User>>();
        services.AddScoped<FormService>();
        services.AddScoped<IRepository<Answer>, Repository<Answer>>();
        services.AddScoped<AnswerService>();
        services.AddScoped<IRepository<Topic>, Repository<Topic>>();
        services.AddScoped<TopicService>();
        services.AddScoped<IRepository<Tag>, Repository<Tag>>();
        services.AddScoped<TagService>();
        services.AddScoped<TemplateSettingsService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<QuestionsSettingsService>();
        services.AddScoped<SnackbarFacade>();
        services.AddImageService();
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
            options.AccessDeniedPath = "/accessdenied";
            options.LoginPath = "/signin";
            options.LogoutPath = "/logout";
        });
        return services;
    }

    public static IServiceCollection AddAll(this IServiceCollection services)
    {
        services
            .AddBlazoredLocalStorage()
            .AddCascadingAuthenticationState()
            .AddMudServices()
            .AddLocalization()
            .ConfigureDbContext()
            .AddIdentityConfig()
            .AddCookieConfig()
            .AddBlazor()
            .AddServices();
        return services;
    }
}
