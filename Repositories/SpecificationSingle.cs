using System.Linq.Expressions;

namespace Forms.Repositories;

public class SpecificationSingle<T> : ISpecificationSingle<T>
{
    public Expression<Func<T, bool>>? Criteria { get; }

    public List<Expression<Func<T, object>>> Includes { get; } = [];

    public void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    public SpecificationSingle(Expression<Func<T, bool>>? criteria = null)
    {
        Criteria = criteria;
    }
}