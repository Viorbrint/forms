using Forms.Data;
using Forms.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Forms.Repositories;

public class TemplateRepository
{
    private ApplicationDbContext Context { get; }

    private DbSet<Template> Templates { get; }

    public TemplateRepository(ApplicationDbContext context)
    {
        Context = context;
        Templates = context.Set<Template>();
    }

    public async Task<Template?> GetByIdAsync(string id)
    {
        return await Templates.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Template>> GetByUserAsync(string userId)
    {
        return await Templates.Where(t => t.Author.Id == userId).ToListAsync();
    }

    public async Task<List<Template>> GetAllAsync()
    {
        return await Templates.ToListAsync();
    }

    public async Task<List<Template>> GetBySpecificationAsync(
        ISpecification<Template> specification
    )
    {
        var result = Templates.AsQueryable();
        if (specification.Criteria != null)
        {
            result = result.Where(specification.Criteria);
        }
        if (specification.OrderBy != null)
        {
            result = specification.OrderBy(result);
        }
        result = specification.Includes.Aggregate(
            result,
            (current, include) => current.Include(include)
        );
        return await result.ToListAsync();
    }

    public async Task AddAsync(Template template)
    {
        Templates.Add(template);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Template template)
    {
        Templates.Update(template);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(string id)
    {
        var template = await GetByIdAsync(id);
        if (template != null)
        {
            Templates.Remove(template);
            await Context.SaveChangesAsync();
        }
    }

    public async Task DeleteByIdsAsync(IEnumerable<string> ids)
    {
        var templates = await Templates.Where(t => ids.Contains(t.Id)).ToListAsync();
        if (templates.Any())
        {
            Templates.RemoveRange(templates);
            await Context.SaveChangesAsync();
        }
    }
}
