using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Specification;

namespace BuildingBlocks.Domain.Data;

public interface IReadOnlyRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetByIdAsync(Guid id);

    Task<TEntity?> FindOneAsync(ISpecification<TEntity> spec);

    Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> spec);
    
    Task<(IEnumerable<TEntity>, int)> FindWithTotalCountAsync(ISpecification<TEntity> spec);
}