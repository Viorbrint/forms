using Forms.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Forms.Components.Pages.UserPage;

public partial class SalesforceFormDialog : ComponentBase
{
    [Inject]
    private ICurrentUserService CurrentUserService { get; set; } = null!;

    [Inject]
    private SnackbarFacade SnackbarFacade { get; set; } = null!;

    [CascadingParameter]
    private MudDialogInstance Dialog { get; set; } = null!;
    private bool IsValid { get; set; }
    private UserModel UserModel { get; } = new();

    private async Task SubmitToSalesforce()
    {
        try
        {
            await CurrentUserService.SyncWithSalesforce(
                UserModel.AccountName,
                UserModel.FirstName,
                UserModel.LastName,
                UserModel.Email
            );
            Dialog.Close(DialogResult.Ok(true));
            SnackbarFacade.Success("Successfully submitted to Salesforce");
        }
        catch (SalesforceException ex)
        {
            Dialog.Close(ex);
            SnackbarFacade.Error(ex.Message);
        }
    }

    private void Cancel()
    {
        Dialog.Cancel();
    }
}

public class UserModel
{
    public string AccountName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
