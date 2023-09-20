using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.EfCore;

public class DbContextFactory : IDisposable
{
    private bool _disposed;
    public DbContext DbContext { get; set; }

    public DbContextFactory(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
            DbContext.Dispose();
        }
    }
}