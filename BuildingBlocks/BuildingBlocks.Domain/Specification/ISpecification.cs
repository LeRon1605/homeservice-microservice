using System.Linq.Expressions;

namespace BuildingBlocks.Domain.Specification;

public interface ISpecification<T>
{
    List<Expression<Func<T, bool>>> Filters { get; }
    
    string? SearchTerm { get; }
    List<string> SearchFields { get; }
    
    bool IsNoTracking { get; set; }
    
    List<Expression<Func<T, object>>> Includes { get; }
    
    List<string> IncludeStrings { get; }
    
    string? OrderByField { get; }
    
    public bool IsDescending { get; }
    
    public int Take { get; }
    
    public int Skip { get; }
}