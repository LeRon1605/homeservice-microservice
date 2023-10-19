using System.Linq.Expressions;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Models;
using BuildingBlocks.Domain.Models.Interfaces;
using BuildingBlocks.Domain.Specification;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore.DynamicLinq;

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

    public async Task<IList<TEntity>> FindListAsync(ISpecification<TEntity> spec)
    {
        return await GetQuery<TEntity>.From(DbSet, spec).ToListAsync(); 
    }

    public async Task<int> CountAsync(ISpecification<TEntity> specification)
    {
        return await GetQuery<TEntity>.From(DbSet, specification).CountAsync();
    }

    public Task<int> CountAsync()
    {
        return DbSet.CountAsync();
    }

    public async Task<int> SumAsync(ISpecification<TEntity> specification, Expression<Func<TEntity, int>> selector) {
        return await GetQuery<TEntity>.From(DbSet, specification).SumAsync(selector);
    }
    
    public async Task<decimal> SumAsync(ISpecification<TEntity> specification, Expression<Func<TEntity, decimal>> selector) {
        return await GetQuery<TEntity>.From(DbSet, specification).SumAsync(selector);
    }
    
    public async Task<IList<TEntity>> GetAllAsync()
    {
        return await DbSet.AsNoTracking().ToListAsync();
    }

    public async Task<(IList<TEntity>, int)> FindWithTotalCountAsync(ISpecification<TEntity> spec)
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

    public Task<bool> AnyAsync()
    {
        return DbSet.AsNoTracking().AnyAsync();
    }

    public Task<bool> AnyAsync(Guid id)
    {
        return DbSet.AsNoTracking().AnyAsync(e => e.Id == id);
    }
}