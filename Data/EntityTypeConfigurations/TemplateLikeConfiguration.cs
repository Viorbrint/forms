using forms.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forms.Data.EntityTypeConfigurations;

public class TemplateLikeConfiguration : IEntityTypeConfiguration<TemplateLike>
{
    public void Configure(EntityTypeBuilder<TemplateLike> builder)
    {
        builder.HasKey(tl => new { tl.UserId, tl.TemplateId });
    }
}
