using Forms.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Forms.Components.Pages.Templates.Edit;

public partial class Questions : ComponentBase
{
    [Parameter]
    public string TemplateId { get; set; } = null!;

    [Inject]
    private QuestionsSettingsService QuestionsSettings { get; set; } = null!;

    private MudDropContainer<DropItem> _dropContainer = null!;

    private void ItemUpdated(MudItemDropInfo<DropItem> dropItem)
    {
        Console.WriteLine(dropItem.IndexInZone);
    }

    private int _i = 1;

    protected override async Task OnInitializedAsync()
    {
        QuestionsSettings.Initialize(TemplateId);
        await QuestionsSettings.Load();
    }

    private void AddQuestion()
    {
        if (_dropContainer == null)
        {
            return;
        }
        _items.Add(new DropItem() { Name = "Item 2", Selector = $"{++_i}" });
        _dropContainer.Refresh();
    }

    private List<DropItem> _items = [new DropItem() { Name = "Item 1", Selector = $"1" }];

    public class DropItem
    {
        public string Name { get; init; } = string.Empty;
        public string Selector { get; set; } = string.Empty;
    }

    private async Task Save()
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
