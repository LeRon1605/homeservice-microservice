using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;
using Products.Domain.ProductAggregate;
using Products.Infrastructure.EfCore.Data;
using Products.Infrastructure.EfCore.Repositories;

namespace Products.API.Extensions;

public static class RepositoryExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Product>, ProductRepository>();
        services.AddScoped<IReadOnlyRepository<Product>, ProductReadonlyRepository>();
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork<ProductDbContext>>();
    }   
}