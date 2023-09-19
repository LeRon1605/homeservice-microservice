using BuildingBlocks.Presentation.Grpc.Proto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Presentation.Authentication;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddHomeServiceAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // {
        //     "GrpcUrls":
        //     {
        //         "Identity": "..."
        //     }
        // }
        
        services.AddGrpcClient<AuthProvider.AuthProviderClient>((provider, options) =>
        {
            var identityUrl = configuration["GrpcUrls:Identity"];
            options.Address = new Uri(identityUrl);
        });
        
        services.AddAuthentication(HomeServiceAuthenticationDefaults.AuthenticationScheme)
                .AddScheme<HomeServiceAuthenticationOption, HomeServiceAuthenticationHandler>(HomeServiceAuthenticationDefaults.AuthenticationScheme, null);

        return services;
    }
}