using forms.Data.Entities;
using Forms.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Forms.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<User>(options)
{
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<QuestionType> QuestionTypes { get; set; } = null!;
    public DbSet<Form> Forms { get; set; } = null!;
    public DbSet<Template> Templates { get; set; } = null!;
    public DbSet<TemplateLike> TemplateLikes { get; set; } = null!;
    public DbSet<TemplateAccess> TemplateAccesses { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Topic> Topics { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<SingleLineAnswer> SingleLineAnswers { get; set; } = null!;
    public DbSet<SingleLineAnswer> MultiLineAnswers { get; set; } = null!;
    public DbSet<NumberAnswer> NumberAnswers { get; set; } = null!;
    public DbSet<BooleanAnswer> BooleanAnswers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
