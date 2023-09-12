using System.Linq.Expressions;

namespace BuildingBlocks.Domain.Specification;

public abstract class Specification<T> : ISpecification<T>
{
    public bool IsTracking { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<string> IncludeStrings { get; } = new();
    public List<string> SearchFields { get; } = new();
    public string? SearchTerm { get; private set; }
    public string? OrderByField { get; private set; }
    public bool IsDescending { get; private set; }
    public int Take { get; private set; }
    public int Skip { get; private set; }


    public bool IsSatisfiedBy(T entity) => ToExpression().Compile().Invoke(entity);
    
    public abstract Expression<Func<T, bool>> ToExpression();

    public ISpecification<T> And(ISpecification<T> specification) => new AndSpecification<T>(this, specification);
    
    protected void SetTracking(bool isTracking) => IsTracking = isTracking;

    protected void AddInclude(Expression<Func<T, object>> includeExpression) => Includes.Add(includeExpression);

    protected void AddInclude(string includeString) => IncludeStrings.Add(includeString);

    protected void AddSearchField(string searchField) => SearchFields.Add(searchField);

    protected void AddSearchTerm(string searchTerm) => SearchTerm = searchTerm.ToLower().Trim();

    protected void AddOrderByField(string? orderBy) => OrderByField = orderBy;

    protected void ApplyDescending() => IsDescending = true;

    protected void ApplyPaging(int pageIndex, int pageSize)
    {
        Take = pageSize;
        Skip = (pageIndex - 1) * pageSize;
    }
    
    
}