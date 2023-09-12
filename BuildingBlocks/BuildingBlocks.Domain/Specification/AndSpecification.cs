using System.Linq.Expressions;

namespace BuildingBlocks.Domain.Specification;


public class AndSpecification<T> : Specification<T>, ISpecification<T>
{
	private readonly ISpecification<T> _leftSpecification;
	private readonly ISpecification<T> _rightSpecification;

	public AndSpecification(ISpecification<T> leftSpecification, ISpecification<T> rightSpecification)
	{
		_leftSpecification = leftSpecification;
		_rightSpecification = rightSpecification;
	}

	public override Expression<Func<T, bool>> ToExpression()
	{
		var leftExpression = _leftSpecification.ToExpression();
		var rightExpression = _rightSpecification.ToExpression();

		var parameter = Expression.Parameter(typeof(T));
		var body = Expression.AndAlso(
			Expression.Invoke(leftExpression, parameter),
			Expression.Invoke(rightExpression, parameter)
		);

		return Expression.Lambda<Func<T, bool>>(body, parameter);
	}
}