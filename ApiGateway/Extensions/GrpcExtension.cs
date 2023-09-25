using ApiGateway.Grpc.Interceptors;
using Products.Application.Grpc.Proto;
using Shopping.Application.Grpc.Proto;

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
        //         "Product": "...",
        //         "Shopping": "..."
        //     }
        // }

        services.AddTransient<AuthorizationHeaderInterceptor>();

        services.AddGrpcClient<ProductGrpcService.ProductGrpcServiceClient>((provider, options) =>
        {
            var productUrl = configuration["GrpcUrls:Product"];
            options.Address = new Uri(productUrl);
        });

        services.AddGrpcClient<ShoppingGrpcService.ShoppingGrpcServiceClient>((provider, options) =>
        {
            var shoppingUrl = configuration["GrpcUrls:Shopping"];
            options.Address = new Uri(shoppingUrl);
        });

        return services;
    }
}