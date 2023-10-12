using BuildingBlocks.Application.Seeder;
using Employees.Application.Seeder;

namespace Employees.API.Extensions;

public static class DataSeederExtension
{
    public static IServiceCollection AddDataSeeder(this IServiceCollection services)
    {
        services.AddScoped<IDataSeeder, EmployeeDataSeeder>();
        
        return services;
    }
}