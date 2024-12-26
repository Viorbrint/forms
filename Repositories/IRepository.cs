namespace Forms.Repositories;

public interface IRepository<T>
    where T : class
{
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task DeleteBySpecificationAsync(ISpecification<T> specification);
    Task DeleteBySpecificationSingleAsync(ISpecificationSingle<T> specification);
    Task<IEnumerable<T>> GetBySpecificationAsync(ISpecification<T> specification);
    Task<T?> GetBySpecificationSingleAsync(ISpecificationSingle<T> specification);
    Task UpdateAsync(T entity);
    Task UpdateRangeAsync(IEnumerable<T> entities);
}
