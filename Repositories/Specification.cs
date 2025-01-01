using System.Linq.Expressions;

namespace Forms.Repositories;

public class Specification<T>(
    Expression<Func<T, bool>>? criteria = null,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null
) : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; } = criteria;

    public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; } = orderBy;

    public List<Expression<Func<T, object>>> Includes { get; } = [];

    public int? Take { get; private set; }

    public int? Skip { get; private set; }

    public void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
    }

    public void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}
