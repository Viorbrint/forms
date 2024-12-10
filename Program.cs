using Forms.Components;
using Forms.Configuration;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder
    .Services.AddMudServices()
    .AddApi()
    .AddCorsConfig()
    .ConfigureDbContext()
    .AddIdentityConfig()
    .AddCookieConfig()
    .AddBlazor()
    .AddServices();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

await app.InitializeAdminRole();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();
app.UseAuth();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
