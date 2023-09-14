using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Presentation.ExceptionHandlers;

public static class ExceptionHandlerExtension
{
    public static IApplicationBuilder UseApplicationExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

                var exception = exceptionHandlerPathFeature?.Error;

                if (exception != null)
                {
                    using var scope = app.ApplicationServices.CreateScope();

                    var exceptionHandler = scope.ServiceProvider.GetRequiredService<IExceptionHandler>();
                    await exceptionHandler.HandleAsync(context, exception);
                }
            });
        });

        return app;
    }
    
    public static IServiceCollection AddApplicationExceptionHandler(this IServiceCollection services)
    {
        services.AddScoped<IExceptionHandler, ExceptionHandler>();

        return services;
    }
}