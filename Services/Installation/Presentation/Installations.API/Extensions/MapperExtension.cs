
using System.Reflection;
using Installations.Application.Commons;

namespace Installations.API.Extensions;


public static class MapperExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(IInstallationApplicationMarker)));
        return services;
    }
}