using System.Security.Claims;
using BuildingBlocks.Application.Identity;
using Grpc.Core;
using IAC.Application.Grpc.Proto;
using IAC.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IAC.Application.Grpc.Services;

public class AuthGrpcService : AuthProvider.AuthProviderBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUser _currentUser;

    public AuthGrpcService(
        UserManager<ApplicationUser> userManager,
        ICurrentUser currentUser)
    {
        _userManager = userManager;
        _currentUser = currentUser;
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

    private async Task<IEnumerable<Claim>> GetUserClaims(ApplicationUser user)
    {
        var claims = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
        };

        foreach (var role in await _userManager.GetRolesAsync(user))
        {
            claims.Add(new(ClaimTypes.Role, role));
        }

        return claims;
    }
}