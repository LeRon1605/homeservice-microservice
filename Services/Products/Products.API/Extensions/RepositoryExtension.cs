using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;
using Products.Infrastructure.EfCore.Data;

namespace Products.API.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork<ProductDbContext>>();
        
        return services;
    }   
}