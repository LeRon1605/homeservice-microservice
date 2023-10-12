using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;
using Installations.Infrastructure;

namespace Installations.API.Extensions;

public static class RepositoryExtension
{
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork<InstallationDbContext>>();
        
        return services;
    }
}