using MudBlazor;

namespace Forms.Services;

public class SnackbarFacade(ISnackbar snackbar)
{
    private ISnackbar Snackbar => snackbar;

    public void Error(string message)
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

    public void Success(string message)
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
}
