using System.Reflection;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;
using Customers.Application;
using Customers.Application.Seeders;
using Customers.Domain.CustomerAggregate;
using Customers.Infrastructure.EfCore;
using Customers.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Customers.API.Extensions;

public static class IServiceCollectionExtension
{
	public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
	{
		services.AddDbContext<CustomerDbContext>(options =>
		{
			options.EnableSensitiveDataLogging(env.IsDevelopment());
			options.UseSqlServer(configuration.GetConnectionString("CustomerDb"));
		});

		return services;
	}

	public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
		services.AddScoped<IReadOnlyRepository<Customer>, ReadOnlyCustomerRepository>();
		services.AddScoped<IUnitOfWork, EfCoreUnitOfWork<CustomerDbContext>>();
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
        });

        return services;
    }
    
    public static IServiceCollection AddSeeder(this IServiceCollection services)
	{
		services.AddScoped<IDataSeeder, CustomerSeeder>();
		
		return services;
	}
}