using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Specification;

namespace BuildingBlocks.Domain.Data;


public interface IRepository<TEntity> where TEntity : AggregateRoot
{
    void Add(TEntity entity);

    Task AddAsync(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    Task<TEntity?> GetByIdAsync(Guid id);

    Task<TEntity?> FindOneAsync(ISpecification<TEntity> spec);

    Task<IEnumerable<TEntity>> FindAsync(ISpecification<TEntity> spec);
    
    Task<bool> AnyAsync(ISpecification<TEntity> spec);
    
    Task<bool> AnyAsync(Guid id);
}