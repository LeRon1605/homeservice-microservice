using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Specification;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.EfCore.Repositories;

public class EfCoreRepository<TDbContext, TAggregateRoot> : IRepository<TAggregateRoot>
    where TAggregateRoot : AggregateRoot
    where TDbContext : DbContext
{
    private DbSet<TAggregateRoot>? _dbSet;
    
    private readonly TDbContext _dbContext;
    private DbSet<TAggregateRoot> DbSet => _dbSet ??= _dbContext.Set<TAggregateRoot>();
    
    public EfCoreRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(TAggregateRoot entity) => DbSet.Add(entity);

    public async Task AddAsync(TAggregateRoot entity) => await DbSet.AddAsync(entity);

    public void Update(TAggregateRoot entity) => DbSet.Update(entity);

    public void Delete(TAggregateRoot entity) => DbSet.Remove(entity);

    public Task<TAggregateRoot?> GetByIdAsync(Guid id)
    {
        return DbSet.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<TAggregateRoot?> FindAsync(ISpecification<TAggregateRoot> spec)
    {
        return await GetQuery<TAggregateRoot>.From(DbSet.AsQueryable(), spec).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TAggregateRoot>> FindListAsync(ISpecification<TAggregateRoot> spec)
    {
        return await GetQuery<TAggregateRoot>.From(DbSet, spec).ToListAsync();
    }

    public async Task<bool> AnyAsync(ISpecification<TAggregateRoot> spec)
    {
        return await GetQuery<TAggregateRoot>.From(DbSet, spec).AnyAsync();
    }

    public Task<bool> AnyAsync(Guid id)
    {
        return DbSet.AnyAsync(e => e.Id == id);
    }
}