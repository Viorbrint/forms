using Forms.Data.Entities;
using Forms.Models.TemplateModels;
using Forms.Services;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.FormsEdit;

public partial class FormsEdit : ComponentBase
{
    [Parameter]
    public string FormId { get; set; } = null!;

    [Inject]
    private ICurrentUserService CurrentUserService { get; set; } = null!;

    [Inject]
    private FormService FormService { get; set; } = null!;

    [Inject]
    private AnswerService AnswerService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    private bool IsReadOnly = true;

    private List<QuestionModel> Questions { get; set; } = [];

    private bool IsLoading { get; set; } = false;

    private string UserId { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        var form = await FormService.GetByIdAsync(FormId);
        if (form == null)
        {
            NavigationManager.NavigateTo("/notfound");
            return;
        }
        var canView = CurrentUserService.CurrentUserCanViewForm(form);
        var canEdit = CurrentUserService.CurrentUserCanEditForm(form);
        if (!canEdit && !canView)
        {
            NavigationManager.NavigateTo("/accessdenied");
            return;
        }
        if (canEdit)
        {
            IsReadOnly = false;
        }

        var answers = await AnswerService.GetByFormAsync(FormId);

        Questions = answers
            .Select(a => new QuestionModel()
            {
                Text = a.Question.QuestionText,
                QuestionId = a.QuestionId,
                Type = a.Type,
                NumberAnswer = (a as NumberAnswer)?.AnswerNumber ?? default(int),
                BooleanAnswer = (a as BooleanAnswer)?.AnswerBoolean ?? default(bool),
                TextAnswer =
                    (a as SingleLineAnswer)?.AnswerText
                    ?? (a as MultiLineAnswer)?.AnswerText
                    ?? string.Empty,
            })
            .ToList();
        IsLoading = false;
    }

    private async Task ChangeForm()
    {
        var answers = await AnswerService.GetByFormAsync(FormId);
        foreach (var answer in answers)
        {
            var question = Questions.First(q => q.QuestionId == answer.QuestionId);
            if (answer is NumberAnswer)
            {
                (answer as NumberAnswer)!.AnswerNumber = question.NumberAnswer;
            }
            else if (answer is BooleanAnswer)
            {
                (answer as BooleanAnswer)!.AnswerBoolean = question.BooleanAnswer;
            }
            else if (answer is SingleLineAnswer)
            {
                (answer as SingleLineAnswer)!.AnswerText = question.TextAnswer;
            }
            else if (answer is MultiLineAnswer)
            {
                (answer as MultiLineAnswer)!.AnswerText = question.TextAnswer;
            }
        }
        await AnswerService.UpdateRangeAsync(answers);
    }
}
