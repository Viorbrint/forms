using Forms.Data.Entities;
using Forms.Models.TemplateModels;
using Forms.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Forms.Components.Pages.Templates.Edit;

public partial class Questions : ComponentBase
{
    [Parameter]
    public string TemplateId { get; set; } = null!;

    [Inject]
    private QuestionsSettingsService QuestionsSettingsService { get; set; } = null!;
    private MudDropContainer<QuestionSettingsModel> _dropContainer = null!;

    private bool IsLoading = false;

    private void ItemUpdated(MudItemDropInfo<QuestionSettingsModel> dropItem)
    {
        var question = dropItem.Item!;
        var requiredIndex = dropItem.IndexInZone;
        var currentIndex = QuestionsSettingsService.Questions.IndexOf(question);
        while (currentIndex != requiredIndex)
        {
            if (currentIndex < requiredIndex)
            {
                (
                    QuestionsSettingsService.Questions[currentIndex],
                    QuestionsSettingsService.Questions[currentIndex + 1]
                ) = (
                    QuestionsSettingsService.Questions[currentIndex + 1],
                    QuestionsSettingsService.Questions[currentIndex]
                );
                currentIndex++;
            }
            else
            {
                (
                    QuestionsSettingsService.Questions[currentIndex],
                    QuestionsSettingsService.Questions[currentIndex - 1]
                ) = (
                    QuestionsSettingsService.Questions[currentIndex - 1],
                    QuestionsSettingsService.Questions[currentIndex]
                );
                currentIndex--;
            }
        }
    }

    protected override async Task OnInitializedAsync()
    {
        QuestionsSettingsService.Initialize(TemplateId);
        IsLoading = true;
        await QuestionsSettingsService.Load();
        IsLoading = false;
    }

    private void AddQuestion()
    {
        if (_dropContainer == null)
        {
            return;
        }
        QuestionsSettingsService.AddQuestion();
        _dropContainer.Refresh();
    }

    private void DeleteQuestion(QuestionSettingsModel item)
    {
        QuestionsSettingsService.DeleteQuestion(item);
        _dropContainer.Refresh();
    }

    private async Task Save()
    {
        await QuestionsSettingsService.Save();
    }
}
