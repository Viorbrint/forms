using Forms.Data.Entities;
using Forms.Services;
using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.Templates.Fill;

public partial class TemplatesFill : ComponentBase
{
    [Parameter]
    public string TemplateId { get; set; } = null!;

    private Template template = new();

    [Inject]
    private ICurrentUserService CurrentUserService { get; set; } = null!;

    [Inject]
    private TemplateService TemplateService { get; set; } = null!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    private bool IsLoading { get; set; } = false;

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        var template = await TemplateService.GetByIdAsync(TemplateId);
        IsLoading = false;
        if (template == null)
        {
            NavigationManager.NavigateTo("/notfound");
            return;
        }

        var canFill = CurrentUserService.CurrentUserCanFill(template);
        if (!canFill)
        {
            NavigationManager.NavigateTo("/accessdenied");
            return;
        }
    }

    protected override void OnInitialized()
    {
        // Simulate fetching the form by ID (replace with real data fetching logic)
        template = MockFormData.FirstOrDefault(f => f.Id == TemplateId) ?? new Template();
    }

    private void SubmitForm()
    {
        // Handle form submission logic
        Console.WriteLine("Form submitted:");
        foreach (var question in template.Questions)
        {
            Console.WriteLine($"Question: {question.Title}, Answer: {question.Answer}");
        }
    }

    private List<Template> MockFormData = new()
    {
        new Template
        {
            Id = "727ce760-c4c5-483c-82a3-7a20b985466a",
            Title = "Job Application Form",
            Description =
                @"# Job Application Form

Welcome to the **Job Application Form**. Please fill out the required fields below to apply for the position. Make sure to provide accurate and up-to-date information.
",
            Questions = new List<Question>
            {
                new Question { Title = "Name", Type = QuestionType.SingleLine },
                new Question { Title = "Experience", Type = QuestionType.Number },
                new Question { Title = "Additional Information", Type = QuestionType.MultiLine },
                new Question { Title = "Agree to Terms", Type = QuestionType.Boolean },
            },
            Topic = "Other",
            Tags = ["awesome", "simple", "template"],
        },
    };

    private class Template
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Question> Questions { get; set; } = new();
        public List<string> Tags { get; set; } = [];
        public string Topic { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = "https://via.placeholder.com/150";
    }

    private class Question
    {
        public string Title { get; set; } = string.Empty;
        public QuestionType Type { get; set; }
        public string Answer { get; set; } = string.Empty;
        public int IntegerAnswer { get; set; }
        public bool IsChecked { get; set; }
    }
}
