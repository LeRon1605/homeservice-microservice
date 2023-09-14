using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.EfCore.Data;

namespace Products.API.Extensions;

public static class MigrationExtension
{
    public static async Task ApplyMigrationAsync(this IApplicationBuilder app, ILogger logger)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            logger.LogInformation("Migrating pending migration...");   

            await context.Database.MigrateAsync();

            logger.LogInformation("Migrated successfully!");
        }
        else
        {
            logger.LogInformation("No pending migration!");
        }
    }
}