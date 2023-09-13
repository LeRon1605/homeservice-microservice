using BuildingBlocks.Application.Behaviors;
using Products.Application.MappingProfiles;

namespace Products.API.Extensions;

public static class MediatRExtension
{
    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Profiles).Assembly);

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
    } 
}