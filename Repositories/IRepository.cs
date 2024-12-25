using System.Linq.Expressions;

namespace Forms.Repositories;

public interface IRepository<T>
    where T : class
{
    Task AddAsync(T entity);
    Task DeleteBySpecificationAsync(ISpecification<T> specification);
    Task DeleteOneByCriteriaAsync(Expression<Func<T, bool>> criteria);
    Task<IEnumerable<T>> GetBySpecificationAsync(ISpecification<T> specification);
    Task<T?> GetOneByCriteriaAsync(Expression<Func<T, bool>> criteria);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(IEnumerable<T> entities);
}
