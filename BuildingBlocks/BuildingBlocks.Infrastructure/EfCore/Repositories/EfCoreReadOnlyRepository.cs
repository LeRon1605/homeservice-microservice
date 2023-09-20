using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Specification;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.EfCore.Repositories;

public class EfCoreReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
    where TEntity : Entity
{
    private DbSet<TEntity>? _dbSet;
    
    private readonly DbContext _dbContext;
    private DbSet<TEntity> DbSet => _dbSet ??= _dbContext.Set<TEntity>();
    
    public EfCoreReadOnlyRepository(DbContextFactory dbContextFactory)
    {
        _dbContext = dbContextFactory.DbContext;
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
        var count = await query.CountAsync();
        var data = await query
                       .Skip(spec.Skip)
                       .Take(spec.Take)
                       .ToListAsync();
        return (data, count); 
    }

    public async Task<bool> AnyAsync(ISpecification<TEntity> specification)
    {
        return await GetQuery<TEntity>.From(DbSet, specification).AnyAsync();
    }
}