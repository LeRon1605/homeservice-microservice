﻿using System.Reflection;
using BuildingBlocks.Domain.Data;
using Customers.Application;
using Customers.Domain.CustomerAggregate;
using Customers.Infrastructure;
using Customers.Infrastructure.Repositories;
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
        services.AddScoped<IReadOnlyCustomerRepository, ReadOnlyCustomerRepository>();
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
}