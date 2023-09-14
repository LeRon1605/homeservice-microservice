using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.EfCore.Data;

namespace Products.API.Extensions;

public static class DbContextExtension
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork<ProductDbContext>>();
        
        services.AddDbContext<ProductDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(ProductDbContext).Assembly.FullName);
                });
            options.EnableSensitiveDataLogging();
        });

        return services;
    } 
}