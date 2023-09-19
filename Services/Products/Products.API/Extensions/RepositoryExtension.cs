using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;
using Products.Domain.MaterialAggregate;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductGroupAggregate;
using Products.Domain.ProductTypeAggregate;
using Products.Domain.ProductUnitAggregate;
using Products.Infrastructure.EfCore.Data;
using Products.Infrastructure.EfCore.Repositories;

namespace Products.API.Extensions;

public static class RepositoryExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Product>, ProductRepository>();
        services.AddScoped<IReadOnlyRepository<Product>, ProductReadOnlyRepository>();
        services.AddScoped<IReadOnlyRepository<ProductGroup>, ProductGroupReadOnlyRepository>();
        services.AddScoped<IReadOnlyRepository<ProductType>, ProductTypeReadOnlyRepository>();
        services.AddScoped<IReadOnlyRepository<ProductUnit>, ProductUnitReadOnlyRepository>();
        
        services.AddScoped<IRepository<ProductGroup>, ProductGroupRepository>();
        services.AddScoped<IRepository<ProductUnit>, ProductUnitRepository>();
        services.AddScoped<IRepository<ProductType>, ProductTypeRepository>();
        
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork<ProductDbContext>>();
        
        services.AddScoped<IRepository<Material>, MaterialRepository>();
        services.AddScoped<IReadOnlyRepository<Material>, MaterialReadOnlyRepository>();
    }   
}