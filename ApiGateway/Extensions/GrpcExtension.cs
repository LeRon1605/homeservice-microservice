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
        
        services.AddGrpcClient<ProductGrpcService.ProductGrpcServiceClient>((provider, options) =>
        {
            var productUrl = configuration["GrpcUrls:Product"];
            options.Address = new Uri(productUrl);
        });

        return services;
    }
}