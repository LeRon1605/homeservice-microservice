using BuildingBlocks.Application.Seeder;
using Shopping.Application.Seeders;

namespace Shopping.API.Extensions;

public static class DataSeederExtension
{
    public static IServiceCollection AddDataSeeder(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, ProductReviewDataSeeder>();
        
        return services;
    }
}