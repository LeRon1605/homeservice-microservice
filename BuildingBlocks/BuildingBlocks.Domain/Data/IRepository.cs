using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Specification;

namespace BuildingBlocks.Domain.Data;


public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
{
    void Add(TAggregateRoot aggregate);

    void Update(TAggregateRoot aggregate);

    void Delete(TAggregateRoot aggregate);

    Task<TAggregateRoot?> GetByIdAsync(Guid id);

    Task<TAggregateRoot?> FindAsync(ISpecification<TAggregateRoot> specification);

    Task<IEnumerable<TAggregateRoot>> FindListAsync(ISpecification<TAggregateRoot> specification);
    
    Task<bool> AnyAsync(ISpecification<TAggregateRoot> specification);
    
    Task<bool> AnyAsync(Guid id);

    Task<bool> AnyAsync();
}