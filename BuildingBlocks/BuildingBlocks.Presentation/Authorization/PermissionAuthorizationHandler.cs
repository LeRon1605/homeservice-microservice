using System.Security.Claims;
using BuildingBlocks.Application.Cache;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Presentation.Grpc.Proto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Presentation.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly ICurrentUser _currentUser;
    private readonly AuthProvider.AuthProviderClient _authProviderClient;
    private readonly ICacheService _cacheService;
    private readonly ILogger<PermissionAuthorizationHandler> _logger;

    public PermissionAuthorizationHandler(ICurrentUser currentUser,
                                          AuthProvider.AuthProviderClient authProviderClient,
                                          ICacheService cacheService,
                                          ILogger<PermissionAuthorizationHandler> logger)
    {
        _currentUser = currentUser;
        _authProviderClient = authProviderClient;
        _cacheService = cacheService;
        _logger = logger;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                   PermissionRequirement requirement)
    {
        var roleName = _currentUser.GetClaim(ClaimTypes.Role);

        if (roleName is null)
            return;

        // Permission from redis
        var permissionFromCache = await _cacheService
            .GetCachedDataAsync<IEnumerable<string>>(KeyGenerator.Generate(CacheType.Permission, roleName));
        
        if (permissionFromCache is not null) 
        {
            _logger.LogInformation("Got permissions of role {role} from cache", roleName);
            
            if (permissionFromCache.Contains(requirement.Permission))
                context.Succeed(requirement);
            
            return;
        }

        // Permission from grpc call to IAC service
        var response = await _authProviderClient.GetPermissionsAsync(new RoleName { Value = roleName });
        
        if (response.Permissions.Contains(requirement.Permission))
            context.Succeed(requirement); 
    }
}