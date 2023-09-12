using System.Linq.Expressions;

namespace BuildingBlocks.Domain.Specification;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T entity);
    
    Expression<Func<T, bool>> ToExpression();
    
    ISpecification<T> And(ISpecification<T> specification);
    
    bool IsTracking { get; set; }
    
    List<Expression<Func<T, object>>> Includes { get; }
    
    List<string> IncludeStrings { get; }
    
    List<string> SearchFields { get; }
    
    public string? SearchTerm { get; }
    
    string? OrderByField { get; }
    
    public bool IsDescending { get; }
    
    public int Take { get; }
    
    public int Skip { get; }
}