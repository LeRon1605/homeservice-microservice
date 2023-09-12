using System.Linq.Expressions;

namespace BuildingBlocks.Domain.Specification;


public class Specification<T> : ISpecification<T>
{
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<string> IncludeStrings { get; } = new();
    public List<string> SearchFields { get; } = new();
    public string? SearchTerm { get; private set; }
    public List<Expression<Func<T, bool>>> FilterConditions { get; private set; } = new();
    public string? OrderByField { get; private set; }
    public bool IsDescending { get; private set; }
    public int Take { get; private set; }
    public int Skip { get; private set; }


    protected void AddFilter(Expression<Func<T, bool>> filter)
    {
        FilterConditions.Add(filter);
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected void AddSearchField(string searchField)
    {
        SearchFields.Add(searchField);
    }

    protected void AddSearchTerm(string searchTerm)
    {
        SearchTerm = searchTerm.ToLower().Trim();
    }

    protected void AddOrderByField(string? orderBy)
    {
        OrderByField = orderBy;
    }

    protected void ApplyDescending()
    {
        IsDescending = true;
    }

    protected void ApplyPaging(int take,
        int skip)
    {
        Take = take;
        Skip = skip;
    }
}