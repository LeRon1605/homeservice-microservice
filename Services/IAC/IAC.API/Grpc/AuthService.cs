using System.Security.Claims;
using BuildingBlocks.Application.Identity;
using Grpc.Core;
using IAC.API.Proto;
using IAC.Domain.Entities;
using IAC.Domain.Exceptions.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace IAC.API.Grpc;

public class AuthService : AuthProvider.AuthProviderBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUser _currentUser;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        ICurrentUser currentUser)
    {
        _userManager = userManager;
        _currentUser = currentUser;
    }

    public override async Task GetClaim(Empty empty, IServerStreamWriter<ClaimResponse> responseStream, ServerCallContext context)
    {
        var us1er = context.GetHttpContext().User.Identity.IsAuthenticated;
        var userId = context.GetHttpContext().User.Identity.Name;

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException(userId);
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