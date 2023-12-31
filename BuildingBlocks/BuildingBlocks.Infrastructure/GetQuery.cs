using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Specification;
using System.Linq.Dynamic.Core;
using BuildingBlocks.Domain.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure;

public static class GetQuery<TEntity> where TEntity : Entity
{
    public static IQueryable<TEntity> From(IQueryable<TEntity> query, ISpecification<TEntity>? specification = null)
    {
        if (specification == null)
            return query;
        
        // Check tracking
        query = specification.IsNoTracking ? query.AsNoTracking() : query.AsTracking();

        if (specification.IsIgnoreSoftDelete)
        {
            query = query.IgnoreQueryFilters();
        }
        
        // Filtering
        foreach (var filter in specification.Filters)
        {
            query = query.Where(filter);
        }

        // Include related data
        query = specification.Includes.Aggregate(
            query,
            (current,
             include) => current.Include(include));

        query = specification.IncludeStrings.Aggregate(
            query,
            (current,
             include) => current.Include(include));
        
        // Searching
        if (!string.IsNullOrWhiteSpace(specification.SearchTerm))
        {
            var searchClause = string.Empty;
            foreach (var searchField in specification.SearchFields)
            {
                searchClause += searchClause == string.Empty ? string.Empty : " || ";
                searchClause += $"{searchField} != null && {searchField}.ToLower().Contains(\"{specification.SearchTerm}\")";
            }
            query = query.Where(searchClause);
        }

        // Ordering
        if (specification.OrderByField != null)
        {
            query = specification.IsDescending
                ? query.OrderBy(specification.OrderByField + $" desc, {GetDefaultSorting()}")
                : query.OrderBy(specification.OrderByField + $", {GetDefaultSorting()}");
        }
        else if (typeof(TEntity).IsAssignableTo(typeof(IAuditableEntity)))
        {
            query = query.OrderBy(GetDefaultSorting());    
        }

        return query;
    }

    private static string GetDefaultSorting()
    {
        if (typeof(TEntity).IsAssignableTo(typeof(IAuditableEntity)))
        {
            return $"{nameof(IAuditableEntity.CreatedAt)} desc";
        }

        return string.Empty;
    }
}
