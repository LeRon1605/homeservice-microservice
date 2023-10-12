using Employees.Application;

namespace Employees.API.Extensions;

public static class MediatRExtension
{
    public static IServiceCollection AddCqrs(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(EmployeeApplicationAssemblyReference));
        });

        return services;
    }
}