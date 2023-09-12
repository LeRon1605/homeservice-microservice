using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Specification;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.EfCore.Repositories;

public class EfCoreRepository<TDbContext, TAggregateRoot> : IRepository<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
    where TDbContext : DbContext
{
    private DbSet<TAggregateRoot> _dbSet;
    
    private readonly TDbContext _dbContext;
    private DbSet<TAggregateRoot> DbSet => _dbSet ??= _dbContext.Set<TAggregateRoot>();
    
    public EfCoreRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(TAggregateRoot entity)
    {
        _dbSet.Add(entity);
    }

    public async Task AddAsync(TAggregateRoot entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(TAggregateRoot entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TAggregateRoot entity)
    {
        _dbSet.Remove(entity);
    }

    public Task<TAggregateRoot?> GetByIdAsync(Guid id)
    {
        return DbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public Task<TAggregateRoot?> FindOneAsync(ISpecification<TAggregateRoot> spec)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TAggregateRoot>> FindAsync(ISpecification<TAggregateRoot> spec)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(ISpecification<TAggregateRoot> spec)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(Guid id)
    {
        return DbSet.AnyAsync(e => e.Id == id);
    }
}