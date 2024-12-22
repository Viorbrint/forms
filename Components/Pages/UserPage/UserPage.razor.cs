using Forms.Data;
using Forms.Data.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Forms.Components.Pages.UserPage;

public partial class UserPage : ComponentBase
{
    [Inject]
    private IHttpContextAccessor HttpContextAccessor { get; set; } = null!;

    // TODO: not good
    [Inject]
    private ApplicationDbContext Db { get; set; } = null!;

    private User CurrentUser { get; set; } = null!;

    protected override void OnInitialized()
    {
        var context = HttpContextAccessor.HttpContext;
        if (context is not { User.Identity.Name: not null })
        {
            return;
        }

        var user = Db
            .Users.Include(u => u.Templates)
            .FirstOrDefault(u => u.UserName == context.User.Identity.Name);
        if (user == null)
        {
            return;
        }
        CurrentUser = user;
    }
}

