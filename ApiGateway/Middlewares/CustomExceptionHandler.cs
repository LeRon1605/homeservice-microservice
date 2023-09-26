using System.Net;
using System.Text.Json;
using ApiGateway.Exceptions;
using Grpc.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Middlewares;

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
                var message = exception?.Message ?? "An error occurred on the server.";

                switch (exception)
                {
                    case RpcException rpcException:
                        response.StatusCode = rpcException.StatusCode switch
                        {
                            StatusCode.InvalidArgument => (int)HttpStatusCode.BadRequest,
                            StatusCode.NotFound => (int)HttpStatusCode.NotFound,
                            _ => (int)HttpStatusCode.InternalServerError
                        };
                        message = rpcException.Status.Detail;
                        break;
                    case HttpClientException httpClientException:
                        response.StatusCode = (int)httpClientException.StatusCode;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                
                var pd = new ProblemDetails
                {
                    Title = env.IsDevelopment() ? exception?.GetType().Name : "An error occurred on the server.",
                    Detail = message,
                    Status = response.StatusCode,
                };

                // pd.Extensions.Add("errorCode", GetErrorCode(exception));

                await response.WriteAsync(JsonSerializer.Serialize(pd));
            });
        });
    }
}