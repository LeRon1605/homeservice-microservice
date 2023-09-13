using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.EfCore.Data;

namespace Products.API.Extensions;

public static class MigrationExtension
{
    public static async Task RunMigrateAsync(this IServiceCollection services)
    {
        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
        await dbContext.Database.MigrateAsync();

        await ProductDbContextSeed.SeedAsync(dbContext);
    } 
}