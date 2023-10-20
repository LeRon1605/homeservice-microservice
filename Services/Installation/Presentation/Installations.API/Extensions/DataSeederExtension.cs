using BuildingBlocks.Application.Seeder;
using Installations.Application.Seeder;

namespace Installations.API.Extensions;

public static class DataSeederExtension
{
    public static IServiceCollection AddDataSeeder(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, InstallationDataSeeder>(); 
        return services;
    }
}