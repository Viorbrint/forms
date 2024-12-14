using System.Globalization;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Layout;

public partial class LocaleSelector : ComponentBase
{
    [Inject]
    NavigationManager NavigationManager { get; set; } = null!;

    private string SelectedLanguage { get; set; } = languages[CultureInfo.CurrentUICulture.Name];

    private static Dictionary<string, string> languages = new Dictionary<string, string>()
    {
        ["en-US"] = "English",
        ["ru-RU"] = "Russian",
    };

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
