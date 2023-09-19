using System.Security.Claims;
using System.Text.Encodings.Web;
using BuildingBlocks.Presentation.Grpc.Proto;
using Grpc.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BuildingBlocks.Presentation.Authentication;

public class HomeServiceAuthenticationHandler : AuthenticationHandler<HomeServiceAuthenticationOption>
{
    private readonly AuthProvider.AuthProviderClient _authProviderClient;
    
    public HomeServiceAuthenticationHandler(
        IOptionsMonitor<HomeServiceAuthenticationOption> options, 
        ILoggerFactory logger, 
        UrlEncoder encoder, 
        ISystemClock clock,
        AuthProvider.AuthProviderClient authProviderClient) : base(options, logger, encoder, clock)
    {
        _authProviderClient = authProviderClient;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Context.Request.Headers.ContainsKey("Authorization"))
        {
            Logger.LogInformation("Authenticated failed, access token not provided!");
            return AuthenticateResult.Fail("Access token not provided!");
        }

        var accessToken = Context.Request.Headers["Authorization"].ToString();

        var headers = new Metadata()
        {
            { "Authorization", accessToken }
        };

        var claims = new List<Claim>();
        try
        {
            using var response = _authProviderClient.GetClaim(new Empty(), new CallOptions(headers));
            await foreach (var claim in response.ResponseStream.ReadAllAsync())
            {
                claims.Add(new Claim(claim.Type, claim.Value));
            }
        }
        catch (RpcException ex)
        {
            Logger.LogInformation("Authenticated failed: {Message}!", ex.Message);
            return ex.StatusCode switch
            {
                StatusCode.Unauthenticated => AuthenticateResult.Fail("Invalid token!"),
                StatusCode.NotFound => AuthenticateResult.Fail("User not found!"),
                StatusCode.Unavailable => AuthenticateResult.Fail("Identity service is down!"),
                _ => AuthenticateResult.Fail("Unknown error when processing authentication!")
            };
        }
        
        Logger.LogInformation("Authenticated success!");
        return AuthenticateResult.Success(GetTicket(claims));
    }
    
    private AuthenticationTicket GetTicket(IEnumerable<Claim> claims)
    {
        var identity = new ClaimsIdentity(HomeServiceAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaims(claims);
        
        return new AuthenticationTicket(new ClaimsPrincipal(identity), Scheme.Name);
    }
}