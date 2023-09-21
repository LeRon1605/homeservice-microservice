using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Presentation.EfCore;

public static class MigrationExtension
{
    public static async Task ApplyMigrationAsync<TDbContext>(this IApplicationBuilder app, ILogger logger)
        where TDbContext : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            logger.LogInformation("[{TDbContext}] Migrating pending migration...", typeof(TDbContext).Name);   

            await context.Database.MigrateAsync();

            logger.LogInformation("[{TDbContext}] Migrated successfully!", typeof(TDbContext).Name);
        }
        else
        {
            logger.LogInformation("[{TDbContext}] No pending migration!", typeof(TDbContext).Name);
        }
    }
}