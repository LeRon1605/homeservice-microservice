using System.Security.Claims;
using BuildingBlocks.Application.Identity;
using Google.Protobuf.Collections;
using Grpc.Core;
using IAC.Application.Grpc.Proto;
using IAC.Application.Services.Interfaces;
using IAC.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IAC.Application.Grpc.Services;

public class AuthGrpcService : AuthProvider.AuthProviderBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ICurrentUser _currentUser;
    private readonly IRoleService _roleService;

    public AuthGrpcService(
        UserManager<ApplicationUser> userManager,
        ICurrentUser currentUser,
        IRoleService roleService,
        RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _currentUser = currentUser;
        _roleService = roleService;
        _roleManager = roleManager;
    }
    
    public override async Task GetClaim(Empty empty, IServerStreamWriter<ClaimResponse> responseStream, ServerCallContext context)
    {
        if (!_currentUser.IsAuthenticated)
        {
            throw new RpcException(new Status(StatusCode.Unauthenticated, "User is not authenticated"));    
        }
        
        var userId = _currentUser.Id;

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "User not found!"));
        }

        var claims = await GetUserClaims(user);

        foreach (var claim in claims)
        {
            await responseStream.WriteAsync(new ClaimResponse()
            {
                Type = claim.Type,
                Value = claim.Value
            });
        }
    }
    
    public override async Task<PermissionResponse> GetPermissions(RoleName roleName, ServerCallContext context)
    {
        var permissions = new RepeatedField<string>();
        
        var role = await _roleManager.FindByNameAsync(roleName.Value)
            ?? throw new RpcException(new Status(StatusCode.NotFound, "Role not found!"));
        
        permissions.AddRange(await _roleService.GetPermissionsInRoleAsync(role.Id));

        var response = new PermissionResponse();
        
        response.Permissions.AddRange(permissions);

        return response;
    }

    private async Task<IEnumerable<Claim>> GetUserClaims(ApplicationUser user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
        };

        // Todo: caching
        foreach (var role in await _userManager.GetRolesAsync(user))
        {
            claims.Add(new(ClaimTypes.Role, role));
        }

        return claims;
    }
}