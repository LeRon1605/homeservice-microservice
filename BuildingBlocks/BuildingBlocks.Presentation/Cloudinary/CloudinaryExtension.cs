using BuildingBlocks.Application.ImageUploader;
using BuildingBlocks.Infrastructure.Cloudinary;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Presentation.Cloudinary;

public static class CloudinaryExtension
{
    public static IServiceCollection AddCloudinary(this IServiceCollection services, IConfiguration configuration)
    {
        // {
        //     "Cloudinary":
        //     {
        //         "CloudName": "...",
        //         "ApiKey": "...",
        //         "ApiSecret": "..."
        //     }
        // }

        services.AddScoped<CloudinaryDotNet.Cloudinary>(provider =>
        {
            var cloudinarySection = configuration.GetSection("Cloudinary");
            var account = new Account()
            {
                Cloud = cloudinarySection["CloudName"],
                ApiKey = cloudinarySection["ApiKey"],
                ApiSecret = cloudinarySection["ApiSecret"]
            };
            
            return new CloudinaryDotNet.Cloudinary(account);
        });

        services.AddScoped<IImageUploader, CloudinaryImageUploader>();
        
        return services;
    }
}