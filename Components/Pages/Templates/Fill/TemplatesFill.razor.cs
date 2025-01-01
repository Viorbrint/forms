using Forms.Data.Entities;
using Forms.Models.TemplateModels;
using Forms.Services;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.Templates.Fill;

public partial class TemplatesFill : ComponentBase
{
    [Parameter]
    public string TemplateId { get; set; } = null!;

    [Inject]
    private ICurrentUserService CurrentUserService { get; set; } = null!;

    [Inject]
    private TemplateService TemplateService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    private TemplateModel TemplateModel = new();
    private bool IsLoading { get; set; } = false;
    private string UserId { get; set; } = null!;
    private bool IsUserLikeTemplate { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        var template = await GetEnsureTemplate();
        if (template == null)
        {
            return;
        }

        UserId = CurrentUserService.UserId!;
        IsUserLikeTemplate = CurrentUserService.IsCurrentUserLikesTemplate(template);

        if (!CurrentUserService.CurrentUserCanFill(template))
        {
            // TODO: readonly mode
            NavigationManager.NavigateTo("/accessdenied");
            return;
        }

        TemplateModel = TemplateModel.MapTemplate(template);
        IsLoading = false;
    }

    private async Task ToggleLike()
    {
        var template = await GetEnsureTemplate();
        if (template == null)
        {
            return;
        }
        await TemplateService.ToggleLike(UserId, template);
        IsUserLikeTemplate = TemplateService.IsUserLikeTemplate(UserId, template);
        TemplateModel.Likes = template.Likes.Count();
    }

    private async Task SubmitForm()
    {
        // TODO: ugly
        var form = new Form { UserId = UserId, TemplateId = TemplateId };
        var numberAnswers = TemplateModel
            .Questions.FindAll(q => q.Type == QuestionType.Number)
            .Select(q => new NumberAnswer()
            {
                AnswerNumber = q.NumberAnswer,
                QuestionId = q.QuestionId,
            })
            .ToList();
        var singleLineAnswers = TemplateModel
            .Questions.FindAll(q => q.Type == QuestionType.SingleLine)
            .Select(q => new SingleLineAnswer()
            {
                AnswerText = q.TextAnswer,
                QuestionId = q.QuestionId,
            })
            .ToList();
        var multiLineAnswers = TemplateModel
            .Questions.FindAll(q => q.Type == QuestionType.MultiLine)
            .Select(q => new MultiLineAnswer()
            {
                AnswerText = q.TextAnswer,
                QuestionId = q.QuestionId,
            })
            .ToList();
        var booleanAnswers = TemplateModel
            .Questions.FindAll(q => q.Type == QuestionType.Boolean)
            .Select(q => new BooleanAnswer()
            {
                AnswerBoolean = q.BooleanAnswer,
                QuestionId = q.QuestionId,
            })
            .ToList();
        form.NumberAnswers = numberAnswers;
        form.SingleLineAnswers = singleLineAnswers;
        form.MultiLineAnswers = multiLineAnswers;
        form.BooleanAnswers = booleanAnswers;
        await TemplateService.AddFormAsync(form, TemplateId);
    }

    private async Task<Template?> GetEnsureTemplate()
    {
        var template = await TemplateService.GetByIdAsync(TemplateId);
        if (template == null)
        {
            NavigationManager.NavigateTo("/notfound");
        }
        return template;
    }
}
