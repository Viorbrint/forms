using System.Linq.Expressions;

namespace Forms.Repositories;

public interface ISpecificationSingle<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
}
