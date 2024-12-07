using Forms.Components;
using Forms.Configuration;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder
    .Services.AddMudServices()
    .ConfigureDbContext()
    .AddIdentityConfig()
    .AddCookieConfig()
    .AddRazorComponents()
    .AddInteractiveServerComponents();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuth();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.Run();
