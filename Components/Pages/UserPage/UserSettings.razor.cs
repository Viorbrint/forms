using Forms.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Forms.Components.Pages.UserPage;

public partial class UserSettings : ComponentBase
{
    [Inject]
    private ICurrentUserService CurrentUserService { get; set; } = null!;

    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    private bool IsSyncedWithSalesforse { get; set; }

    private async Task OpenSalesforceForm()
    {
        var dialog = DialogService.Show<SalesforceFormDialog>("Salesforce");
        var result = await dialog.Result;
        if (result != null && !result.Canceled)
        {
            IsSyncedWithSalesforse = await CurrentUserService.UserIsSyncedWithSalesforce();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        IsSyncedWithSalesforse = await CurrentUserService.UserIsSyncedWithSalesforce();
    }
}
