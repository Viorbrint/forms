using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Forms.Components.Pages.UserPage;

public partial class UserSettings : ComponentBase
{
    [Inject] private IDialogService DialogService { get; set; } = null!;
    private void OpenSalesforceForm()
    {
        DialogService.Show<SalesforceFormDialog>("Salesforce");
    }
}