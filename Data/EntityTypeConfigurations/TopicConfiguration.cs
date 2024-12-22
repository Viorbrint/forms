using Forms.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forms.Data.EntityTypeConfigurations;

public class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    private List<string> TopicNames =
    [
        "Education",
        "Quiz",
        "Feedback",
        "Polls",
        "Health",
        "Job Application",
        "Research",
        "Event Planning",
        "Customer Satisfaction",
        "Product Feedback",
        "Lifestyle",
        "Hobbies and Interests",
        "Entertainment",
        "Travel",
        "Community",
        "Survey",
        "Personal Development",
        "Technology",
        "Sports and Fitness",
        "Other",
    ];

    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        int i = 1;
        builder.HasData(
            TopicNames.Select(name => new Topic { TopicName = name, Id = $"{i++}" }).ToArray()
        );
        builder.HasIndex(t => t.TopicName).IsUnique();
    }
}
