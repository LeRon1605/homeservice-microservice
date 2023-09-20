using BuildingBlocks.Application.Cache;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.Cache;
using BuildingBlocks.Presentation.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Presentation.Extension;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCache(this IServiceCollection services)
    {
        return services.AddScoped<ICacheService, CacheService>();
    }

	public static IServiceCollection AddCurrentUser(this IServiceCollection services)
	{
		services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
	}
	
	public static IServiceCollection AddApplicationCors(this IServiceCollection services)
	{
		services.AddCors(o => o.AddPolicy("HomeService", builder =>
		{
			builder.WithOrigins("*")
				.AllowAnyMethod()
				.AllowAnyHeader();
		}));

		return services;
	}
}