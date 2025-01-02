using Forms.Data.Entities;
using Forms.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Forms.Components.Pages.UserPage;

public partial class UserForms : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    private ICurrentUserService CurrentUserService { get; set; } = null!;

    private IEnumerable<Form> Forms { get; set; } = [];

    private HashSet<Form> SelectedForms { get; set; } = [];

    private string UserId { get; set; } = string.Empty;

    [Inject]
    private FormService FormService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        UserId = CurrentUserService.UserId!;
        await ReloadForms();
    }

    private async Task ReloadForms()
    {
        Forms = await FormService.GetByUserAsync(UserId);
    }

    private void OnFormClick(TableRowClickEventArgs<Form> args)
    {
        var form = args.Item;
        if (form != null)
        {
            NavigateToEdit(form.Id);
        }
    }

    private void NavigateToEdit(string formId)
    {
        var editPath = $"/forms/edit/{formId}";
        NavigationManager.NavigateTo(editPath);
    }

    private async Task DeleteForms()
    {
        var ids = SelectedForms.Select(x => x.Id).ToList();
        await FormService.DeleteByIdsAsync(ids);

        await ReloadForms();
    }

    private void EditSelected()
    {
        var form = SelectedForms.First();
        NavigateToEdit(form.Id);
    }
}
