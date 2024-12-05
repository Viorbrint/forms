using forms.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forms.Data.EntityTypeConfigurations;

public class TemplateAccessConfiguration : IEntityTypeConfiguration<TemplateAccess>
{
    public void Configure(EntityTypeBuilder<TemplateAccess> builder)
    {
        builder.HasKey(ta => new { ta.UserId, ta.TemplateId });
    }
}
