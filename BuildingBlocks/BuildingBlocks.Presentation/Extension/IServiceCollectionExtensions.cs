using BuildingBlocks.Application.Cache;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.Cache;
using BuildingBlocks.Presentation.Authorization;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Presentation.Extension;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCache(this IServiceCollection services)
    {
        return services.AddScoped<ICacheService, CacheService>();
    }

	public static IServiceCollection AddService(this IServiceCollection services)
	{
		return services.AddScoped<ICurrentUser, CurrentUser>();
	}
}