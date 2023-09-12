using BuildingBlocks.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;

public class EfCoreUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;
    private IDbContextTransaction _transaction;
    
    public bool HasActiveTransaction => _transaction != null;
    
    public EfCoreUnitOfWork(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        if (!HasActiveTransaction)
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }
    }

    public async Task CommitTransactionAsync()
    {
        if (HasActiveTransaction)
        {
            try
            {
                await SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (HasActiveTransaction)
        {
            await _transaction.RollbackAsync();
        }
    }
}