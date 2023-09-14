using BuildingBlocks.Application.Seeder;
using Products.Application.Seeders;

namespace Products.API.Extensions;

public static class DataSeederExtension
{
    public static IServiceCollection AddDataSeeder(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, ProductDataSeeder>();
        
        return services;
    }
}