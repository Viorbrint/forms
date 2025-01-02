using System.Linq.Expressions;

namespace Forms.Repositories;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    public int? Take { get; }
    public int? Skip { get; }
}
