using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;
using Shopping.Infrastructure.EfCore;

namespace Shopping.API.Extensions;

public static class RepositoryExtension
{
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork<OrderDbContext>>();
        
        return services;
    }
}