using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;
using Employees.Infrastructure;

namespace Employees.API.Extensions;

public static class RepositoryExtension
{
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork<EmployeeDbContext>>();
        
        return services;
    }
}