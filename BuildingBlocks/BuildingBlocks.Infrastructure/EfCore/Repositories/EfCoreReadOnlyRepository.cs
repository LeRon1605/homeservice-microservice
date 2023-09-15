using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Specification;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.EfCore.Repositories;

public class EfCoreReadOnlyRepository<TDbContext, TEntity> : IReadOnlyRepository<TEntity>
    where TEntity : Entity
    where TDbContext : DbContext
{
    private DbSet<TEntity>? _dbSet;
    
    private readonly TDbContext _dbContext;
    private DbSet<TEntity> DbSet => _dbSet ??= _dbContext.Set<TEntity>();
    
    public EfCoreReadOnlyRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<TEntity?> GetByIdAsync(Guid id) 
        => await DbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

    public async Task<TEntity?> FindAsync(ISpecification<TEntity> spec)
    {
        return await GetQuery<TEntity>.From(DbSet, spec).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> FindListAsync(ISpecification<TEntity> spec)
    {
        return await GetQuery<TEntity>.From(DbSet, spec).ToListAsync(); 
    }

    public async Task<int> CountAsync(ISpecification<TEntity> specification)
    {
        return await GetQuery<TEntity>.From(DbSet, specification).CountAsync();
    }
    
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await DbSet.AsNoTracking().ToListAsync();
    }

    public async Task<(IEnumerable<TEntity>, int)> FindWithTotalCountAsync(ISpecification<TEntity> spec)
    {
        var query = GetQuery<TEntity>.From(DbSet, spec);
        var count = await DbSet.CountAsync(spec.ToExpression());
        var data = await query.ToListAsync();
        return (data, count); 
    }
}