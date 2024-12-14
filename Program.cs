using Forms.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAll();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseDev();
}
await app.UseAll();
app.Run();
