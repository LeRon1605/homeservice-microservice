using BuildingBlocks.Application.Cache;
using BuildingBlocks.Infrastructure.Cache;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Presentation.Extension;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCache(this IServiceCollection services)
    {
        return services.AddScoped<ICacheService, CacheService>();
    }
}