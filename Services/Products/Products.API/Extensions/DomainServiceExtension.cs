using Products.Domain.MaterialAggregate.DomainServices;

namespace Products.API.Extensions;

public static class DomainServiceExtension
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IMaterialDomainService, MaterialDomainService>();
        return services;
    }
}