using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Specification;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.EfCore.Repositories;

public class EfCoreReadOnlyRepository<TDbContext, TAggregateRoot> : IReadOnlyRepository<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
    where TDbContext : DbContext
{
    private DbSet<TAggregateRoot> _dbSet;
    
    private readonly TDbContext _dbContext;
    private DbSet<TAggregateRoot> DbSet => _dbSet ??= _dbContext.Set<TAggregateRoot>();
    
    public EfCoreReadOnlyRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<TAggregateRoot?> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public Task<TAggregateRoot?> FindOneAsync(ISpecification<TAggregateRoot> spec)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TAggregateRoot>> FindAsync(ISpecification<TAggregateRoot> spec)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<TAggregateRoot>, int)> FindWithTotalCountAsync(ISpecification<TAggregateRoot> spec)
    {
        throw new NotImplementedException();
    }
}