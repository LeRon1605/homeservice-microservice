using ApiGateway.Services;
using ApiGateway.Services.Interfaces;

namespace ApiGateway.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        
        return services;
    }
}