using ApiGateway.Services;
using ApiGateway.Services.Interfaces;

namespace ApiGateway.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IShoppingService, ShoppingService>();
        
        return services;
    }

    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        var productHttpUrl = configuration["HttpUrls:Product"] ?? throw new Exception("Product Http Url is null!");
        services.AddHttpClient<IProductService, ProductService>(client => client.BaseAddress = new Uri(productHttpUrl));
        
        return services;
    }
}