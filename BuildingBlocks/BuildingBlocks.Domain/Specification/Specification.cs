using System.Linq.Expressions;

namespace BuildingBlocks.Domain.Specification;

public abstract class Specification<T> : ISpecification<T>
{
    public bool IsNoTracking { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<Expression<Func<T, bool>>> Filters { get; } = new();
    public List<string> IncludeStrings { get; } = new();
    public string? OrderByField { get; private set; }
    public bool IsDescending { get; private set; }
    public int Take { get; private set; }
    public int Skip { get; private set; }

    public List<string> SearchFields { get; } = new();
    public string? SearchTerm { get; private set; }
    

    protected void AddFilter(Expression<Func<T, bool>> filter) => Filters.Add(filter);
    
    protected void AddSearchTerm(string? searchTerm) => SearchTerm = searchTerm?.ToLower().Trim();
    
    protected void AddSearchField(string searchField) => SearchFields.Add(searchField);
    
    protected void SetTracking(bool isTracking) => IsNoTracking = isTracking;

    protected void AddInclude(Expression<Func<T, object>> includeExpression) => Includes.Add(includeExpression);

    protected void AddInclude(string includeString) => IncludeStrings.Add(includeString);

    protected void AddOrderByField(string? orderBy) => OrderByField = orderBy;

    protected void ApplyDescending() => IsDescending = true;

    protected void ApplyPaging(int pageIndex, int pageSize)
    {
        Take = pageSize;
        Skip = (pageIndex - 1) * pageSize;
    }

    
}