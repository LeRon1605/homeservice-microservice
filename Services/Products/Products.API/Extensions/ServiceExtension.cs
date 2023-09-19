using Products.Application.Commands.ProductCommands.Validator;

namespace Products.API.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection service)
    {
        service.AddScoped<IProductValidator, ProductValidator>();
        return service;
    }
}