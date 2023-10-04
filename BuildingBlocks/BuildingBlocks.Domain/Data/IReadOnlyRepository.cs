using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Specification;

namespace BuildingBlocks.Domain.Data;

public interface IReadOnlyRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetByIdAsync(Guid id);
    
    Task<IList<TEntity>> GetAllAsync();

    Task<TEntity?> FindAsync(ISpecification<TEntity> specification);

    Task<IList<TEntity>> FindListAsync(ISpecification<TEntity> specification);
    
    Task<int> CountAsync(ISpecification<TEntity> specification);
    
    Task<(IList<TEntity>, int)> FindWithTotalCountAsync(ISpecification<TEntity> specification);

    Task<bool> AnyAsync(ISpecification<TEntity> specification);
    
    Task<bool> AnyAsync();
}