using BuildingBlocks.Application.Behaviors;
using Contracts.Application.Mapper;
using FluentValidation;

namespace Contracts.API.Extensions;

public static class MediatRExtension
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(MappingProfiles).Assembly);

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssemblyContaining(typeof(MappingProfiles));

        return services;
    } 
}