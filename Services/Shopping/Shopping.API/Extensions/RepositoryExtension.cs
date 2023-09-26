using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;
using Shopping.Domain.OrderAggregate;
using Shopping.Domain.ProductAggregate;
using Shopping.Infrastructure.EfCore;
using Shopping.Infrastructure.EfCore.Repositories;

namespace Shopping.API.Extensions;

public static class RepositoryExtension
{
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork<OrderDbContext>>();
        services.AddScoped<IProductRepository, ProductRepository>();
        
        return services;
    }
}