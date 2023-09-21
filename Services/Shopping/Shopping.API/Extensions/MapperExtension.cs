using System.Reflection;
using Shopping.Application;

namespace Shopping.API.Extensions;


public static class MapperExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(OrderApplicationAssemblyReference)));
        
        return services;
    }
}