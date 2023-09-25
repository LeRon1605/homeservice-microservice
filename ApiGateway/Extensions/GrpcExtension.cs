using ApiGateway.Grpc.Interceptors;
using Products.Application.Grpc.Proto;

namespace ApiGateway.Extensions;

public static class GrpcExtension
{
    public static IServiceCollection AddGrpcClientServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // {
        //     "GrpcUrls":
        //     {
        //         "Identity": "...",
        //         "Product": "..."
        //     }
        // }

        services.AddTransient<AuthorizationHeaderInterceptor>();
        
        services.AddGrpcClient<ProductGrpcService.ProductGrpcServiceClient>((provider, options) =>
        {
            var productUrl = configuration["GrpcUrls:Product"];
            options.Address = new Uri(productUrl);
        }).AddInterceptor<AuthorizationHeaderInterceptor>();

        return services;
    }
}