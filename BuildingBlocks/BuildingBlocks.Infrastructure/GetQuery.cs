using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Specification;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure;

public static class GetQuery<TEntity> where TEntity : Entity
{
    public static IQueryable<TEntity> From(IQueryable<TEntity> query, ISpecification<TEntity>? specification = null)
    {
        if (specification == null)
            return query;
        
        // Check tracking
        query = specification.IsTracking ? query.AsTracking() : query.AsNoTracking();

        // Filtering
        query = query.Where(specification.ToExpression());

        // Include related data
        query = specification.Includes.Aggregate(
            query,
            (current,
             include) => current.Include(include));

        query = specification.IncludeStrings.Aggregate(
            query,
            (current,
             include) => current.Include(include));

        // Ordering
        if (specification.OrderByField != null)
            query = specification.IsDescending
                        ? query.OrderBy(specification.OrderByField + " desc")
                        : query.OrderBy(specification.OrderByField);

        // Paging
        if (specification is { Take: > 0, Skip: >= 0 })
            query = query
                .Skip(specification.Skip)
                .Take(specification.Take);

        return query;
    }
}
