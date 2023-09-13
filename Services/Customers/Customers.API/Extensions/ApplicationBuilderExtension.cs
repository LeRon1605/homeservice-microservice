using Customers.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Customers.API.Extensions;

public static class ApplicationBuilderExtension
{
    public static async Task ApplyMigrationAsync(this IApplicationBuilder app, ILogger logger)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
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