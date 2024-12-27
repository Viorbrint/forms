using Microsoft.AspNetCore.Components;

namespace Forms.Components.Pages.Templates.Fill;

public partial class TemplatesFill : ComponentBase
{
    [Parameter]
    public string? TemplateId { get; set; }

    private Template template = new();

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
                new Question { Title = "Name", Type = QuestionType.Text },
                new Question { Title = "Experience", Type = QuestionType.Integer },
                new Question
                {
                    Title = "Additional Information",
                    Type = QuestionType.MultilineText,
                },
                new Question { Title = "Agree to Terms", Type = QuestionType.Checkbox },
            },
        },
    };

    private class Template
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Question> Questions { get; set; } = new();

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

    private enum QuestionType
    {
        Text,
        MultilineText,
        Integer,
        Checkbox,
    }
}
