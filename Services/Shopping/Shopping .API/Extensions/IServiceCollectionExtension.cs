using System.Reflection;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Shopping.Application;
using Shopping.Domain.ShoppingAggregate;
using Shopping.Infrastructure;
using Shopping.Infrastructure.Repositories;

namespace Shopping.API.Extensions;


public static class IServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddDbContext<OrderDbContext>(options =>
        {
            options.EnableSensitiveDataLogging(env.IsDevelopment());
            options.UseSqlServer(configuration.GetConnectionString("OrderDb"));
        });

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IReadOnlyRepository<Order>, ReadOnlyOrderRepository>();
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