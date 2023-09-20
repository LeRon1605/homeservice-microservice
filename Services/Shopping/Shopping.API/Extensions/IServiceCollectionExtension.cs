using System.Reflection;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;
using Shopping.Application;
using Shopping.Infrastructure.EfCore;

namespace Shopping.API.Extensions;


public static class IServiceCollectionExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork<OrderDbContext>>();
        return services;
    }
    
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(OrderApplicationAssemblyReference)));
        
        return services;
    }
    
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(OrderApplicationAssemblyReference));
        });

        return services;
    }
}