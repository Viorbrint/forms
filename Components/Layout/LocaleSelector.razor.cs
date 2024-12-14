using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Layout;

public partial class LocaleSelector : ComponentBase
{
    [Inject]
    NavigationManager NavigationManager { get; set; } = null!;

    private string SelectedLanguage { get; set; } = CultureInfo.CurrentUICulture.Name;

    private string[] languages = ["en-US", "ru-RU"];

    private bool _isOpen = false;

    private void ChangeLanguage(string language)
    {
        var uri = new Uri(NavigationManager.Uri);
        var query = System.Web.HttpUtility.ParseQueryString(uri.Query);

        query["culture"] = language;

        var newUri = $"{uri.GetLeftPart(UriPartial.Path)}?{query}";

        NavigationManager.NavigateTo(newUri, forceLoad: true);
    }
}
