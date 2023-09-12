using System.Linq.Expressions;

namespace BuildingBlocks.Domain.Specification;

public abstract class Specification<T> : ISpecification<T>
{
	public Specification<T> AndSpecification(Specification<T> specification)
	{
		return new AndSpecification<T>(this, specification);
	}

	public bool IsSatisfiedBy(T entity)
	{
		return ToExpression().Compile().Invoke(entity);
	}

	public abstract Expression<Func<T, bool>> ToExpression();
}