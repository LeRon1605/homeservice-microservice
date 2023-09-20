using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Presentation.EfCore;

public static class EfCoreExtension
{
    public static IServiceCollection AddEfCoreDbContext<TDbContext>(this IServiceCollection services, IConfiguration configuration) 
        where TDbContext : DbContext
    {
        // "ConnectionStrings": {
        //     "Default": ""
        // },
        
        services.AddDbContext<TDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });

        services.AddScoped<DbContextFactory>(provider 
            => new DbContextFactory(provider.GetRequiredService<TDbContext>()));
        
        services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>))
                .AddScoped(typeof(IReadOnlyRepository<>), typeof(EfCoreReadOnlyRepository<>));

        return services;
    }
}