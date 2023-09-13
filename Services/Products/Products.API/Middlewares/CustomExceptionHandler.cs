using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace Products.API.Middlewares;

public static class CustomExceptionHandler
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app,
                                                     IWebHostEnvironment env)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    var response = context.Response;
                    response.ContentType = "application/json";
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    var message = exception?.Message;
    
                    switch (exception)
                    {
                        case ValidationException:
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            message = "Validation error";
                            break;
                        default:
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                    }
    
                    var isDevelopment = env.IsDevelopment();
    
                    var pd = new ProblemDetails
                    {
                        Title = isDevelopment ? message : "An error occurred on the server.",
                        Status = response.StatusCode,
                        Detail = isDevelopment ? exception?.StackTrace : null
                        // Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
                    };
    
                    if (exception is ValidationException validationException)
                        pd.Extensions.Add("errors",
                            validationException.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }));
    
                    pd.Extensions.Add("traceId", context.TraceIdentifier);
    
                    await response.WriteAsync(JsonSerializer.Serialize(pd));
                });
            });
        }
}