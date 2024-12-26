using System.Linq.Expressions;

namespace Forms.Repositories;

public class Specification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; }

    public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; }

    public List<Expression<Func<T, object>>> Includes { get; } = [];

    public void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    public Specification(
        Expression<Func<T, bool>>? criteria = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null
    )
    {
        Criteria = criteria;
        OrderBy = orderBy;
    }
}
