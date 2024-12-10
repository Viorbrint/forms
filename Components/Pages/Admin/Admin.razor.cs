using Forms.Data.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace Forms.Components.Pages.Admin;

public partial class Admin : ComponentBase
{
    [Inject]
    ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    private UserManager<User> UserManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        Users = await UserManager.Users.ToListAsync();
    }

    private IEnumerable<User> Users { get; set; } = [];

    private string _searchString = String.Empty;

    private bool _sortNameByLength = false;

    private Func<User, object> SortBy =>
        user =>
        {
            if (user.UserName == null)
            {
                throw new InvalidOperationException("User should have name and email");
            }
            if (_sortNameByLength)
            {
                return user.UserName;
            }
            else
            {
                return user.UserName;
            }
        };

    private Func<User, bool> QuickFilter =>
        user =>
        {
            if (user.UserName == null || user.Email == null)
            {
                throw new InvalidOperationException("User should have name and email");
            }
            if (string.IsNullOrWhiteSpace(_searchString))
            {
                return true;
            }
            if (user.UserName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            if (user.Email.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        };

    private HashSet<User> SelectedUsers { get; set; } = [];

    private void ShowSuccessSnackbar(string message)
    {
        Snackbar.Add(
            message,
            Severity.Success,
            configure: config =>
            {
                config.ShowCloseIcon = false;
            }
        );
    }

    private void ShowErrorSnackbar(string message)
    {
        Snackbar.Add(
            message,
            Severity.Error,
            configure: config =>
            {
                config.ShowCloseIcon = false;
            }
        );
    }

    // TODO: defend this
    private async Task BlockUsers()
    {
        foreach (var user in SelectedUsers)
        {
            var identityResult = await UserManager.SetLockoutEndDateAsync(
                user,
                DateTimeOffset.MaxValue
            );
            if (!identityResult.Succeeded)
            {
                ShowErrorSnackbar($"Error blocking {user.UserName}");
            }
            else
            {
                ShowSuccessSnackbar($"{user.UserName} is blocked");
            }
        }
        await ReloadUsers();
    }

    private async Task UnblockUsers()
    {
        foreach (var user in SelectedUsers)
        {
            var identityResult = await UserManager.SetLockoutEndDateAsync(user, null);
            if (!identityResult.Succeeded)
            {
                ShowErrorSnackbar($"Error unblocking {user.UserName}");
            }
            else
            {
                ShowSuccessSnackbar($"{user.UserName} is unblocked");
            }
        }
        await ReloadUsers();
    }

    private async Task DeleteUsers()
    {
        foreach (var user in SelectedUsers)
        {
            var identityResult = await UserManager.DeleteAsync(user);
            if (!identityResult.Succeeded)
            {
                ShowErrorSnackbar($"Error deleting {user.UserName}");
            }
            else
            {
                ShowSuccessSnackbar($"{user.UserName} is deleted");
            }
        }
        await ReloadUsers();
    }

    private async Task AddToAdmins()
    {
        foreach (var user in SelectedUsers)
        {
            var identityResult = await UserManager.AddToRoleAsync(user, "Admin");
            if (!identityResult.Succeeded)
            {
                ShowErrorSnackbar($"Error adding administrator role {user.UserName}");
            }
            else
            {
                ShowSuccessSnackbar($"{user.UserName} is added to admins");
            }
        }
        await ReloadUsers();
    }

    private async Task RemoveFromAdmins()
    {
        foreach (var user in SelectedUsers)
        {
            var identityResult = await UserManager.RemoveFromRoleAsync(user, "Admin");
            if (!identityResult.Succeeded)
            {
                ShowErrorSnackbar($"Error removing administrator role {user.UserName}");
            }
            else
            {
                ShowSuccessSnackbar($"{user.UserName} is removed from admins");
            }
        }
        await ReloadUsers();
    }

    private async Task ReloadUsers()
    {
        Users = await UserManager.Users.ToListAsync();
        SelectedUsers = [];
    }
}
