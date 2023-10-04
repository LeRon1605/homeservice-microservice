using BuildingBlocks.Application.Seeder;
using Contracts.Application.Seeders;

namespace Contracts.API.Extensions;

public static class DataSeederExtension
{
    public static IServiceCollection AddDataSeeders(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, ContractDataSeeder>();
        return services;
    }
}