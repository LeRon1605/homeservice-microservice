using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Specification;

namespace BuildingBlocks.Domain.Data;

public interface IReadOnlyRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetByIdAsync(Guid id);
    
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity?> FindAsync(ISpecification<TEntity> specification);

    Task<IEnumerable<TEntity>> FindListAsync(ISpecification<TEntity> specification);
    
    Task<int> CountAsync(ISpecification<TEntity> specification);
    
    Task<(IEnumerable<TEntity>, int)> FindWithTotalCountAsync(ISpecification<TEntity> specification);
}