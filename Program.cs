using Forms.Components;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
