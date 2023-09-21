using Shopping.Application;

namespace Shopping.API.Extensions;

public static class MediatRExtension
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(OrderApplicationAssemblyReference));
        });

        return services;
    }
}