using System.Reflection;
using Contracts.Application.Mapper;

namespace Contracts.API.Extensions;

public static class AutoMapperExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfiles)));
        
        return services;
    }
}