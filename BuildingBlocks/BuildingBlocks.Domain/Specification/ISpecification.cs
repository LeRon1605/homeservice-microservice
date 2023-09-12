using System.Linq.Expressions;

namespace BuildingBlocks.Domain.Specification;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
    Expression<Func<T, bool>> ToExpression();

    Specification<T> AndSpecification(Specification<T> specification);
}