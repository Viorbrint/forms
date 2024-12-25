using Forms.Data;
using Microsoft.EntityFrameworkCore;

namespace Forms.Repositories;

public class Repository<T> : IRepository<T>
    where T : class
{
    private ApplicationDbContext Context { get; }

    private DbSet<T> Entities { get; }

    public Repository(ApplicationDbContext context)
    {
        Context = context;
        Entities = context.Set<T>();
    }

    public async Task AddAsync(T entity)
    {
        await Entities.AddAsync(entity);
        await Context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await Entities.AddRangeAsync(entities);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteBySpecificationAsync(ISpecification<T> specification)
    {
        var entities = await GetBySpecificationAsync(specification);
        Entities.RemoveRange(entities);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteBySpecificationSingleAsync(ISpecificationSingle<T> specification)
    {
        var entity = await GetBySpecificationSingleAsync(specification);
        if (entity == null)
        {
            return;
        }
        Entities.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetBySpecificationAsync(ISpecification<T> specification)
    {
        var result = Entities.AsQueryable();
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

    public async Task<T?> GetBySpecificationSingleAsync(ISpecificationSingle<T> specification)
    {
        var result = Entities.AsQueryable();

        result = specification.Includes.Aggregate(
            result,
            (current, include) => current.Include(include)
        );

        if (specification.Criteria != null)
        {
            result = result.Where(specification.Criteria);
        }

        return await result.FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        Entities.Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        Entities.UpdateRange(entities);
        await Context.SaveChangesAsync();
    }
}
