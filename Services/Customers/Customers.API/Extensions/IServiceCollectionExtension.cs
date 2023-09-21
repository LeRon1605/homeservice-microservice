using System.Reflection;
using BuildingBlocks.Application.Behaviors;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;
using Customers.Application;
using Customers.Application.Seeders;
using Customers.Domain.CustomerAggregate;
using Customers.Infrastructure.EfCore;
using Customers.Infrastructure.EfCore.Repositories;
using FluentValidation;

namespace Customers.API.Extensions;

public static class IServiceCollectionExtension
{
	public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
		services.AddScoped<IUnitOfWork, EfCoreUnitOfWork<CustomerDbContext>>();
		services.AddScoped<ICustomerRepository, CustomerRepository>();
		
		return services;
    }
    
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(CustomerApplicationAssemblyReference)));
        
        return services;
    }
    
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(CustomerApplicationAssemblyReference));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
    
    public static IServiceCollection AddSeeder(this IServiceCollection services)
	{
		services.AddScoped<IDataSeeder, CustomerSeeder>();
		
		return services;
	}
    
    public static IServiceCollection AddValidators(this IServiceCollection services)
	{
		services.AddValidatorsFromAssemblyContaining(typeof(CustomerApplicationAssemblyReference));
		
		return services;
	}
}