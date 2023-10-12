using Installations.Application.Commons;

namespace Installations.API.Extensions;

public static class MediatRExtension
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(IInstallationApplicationMarker));
        });

        return services;
    }
}