using System.Linq.Expressions;

namespace Forms.Repositories;

public class SpecificationSingle<T>(Expression<Func<T, bool>>? criteria = null)
    : ISpecificationSingle<T>
{
    public Expression<Func<T, bool>>? Criteria { get; } = criteria;

    public List<Expression<Func<T, object>>> Includes { get; } = [];

    public void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}
