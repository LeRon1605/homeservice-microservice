using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using IAC.Application.Common;
using IAC.Application.Services.Interfaces;
using IAC.Domain.Entities;
using IAC.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace IAC.Application.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenRepository _tokenRepository;
    private readonly JwtSettings _jwtSettings;


    public TokenService(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings,
                        ITokenRepository tokenRepository)
    {
        _userManager = userManager;
        _tokenRepository = tokenRepository;
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

    public async Task<string> GenerateAccessTokenAsync(string userId)
    {
        // 1. Create header and signature 
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        // 2. Create Jwt
        var tokenOptions = new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            claims: await GetUserClaimListAsync(userId),
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings.AccessTokenExpiryInMinutes)),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    public async Task AddRefreshTokenAsync(string userId, string refreshToken)
    {
        var refreshTokenLifetime = DateTime.UtcNow.AddHours(Convert.ToDouble(_jwtSettings.RefreshTokenExpiryInHours));
        
        await _tokenRepository.AddAsync(userId, refreshToken, refreshTokenLifetime);
    }

    public async Task ValidateRefreshTokenAsync(string refreshToken)
    {
        var refreshTokenLifetime = await _tokenRepository.GetExpiryTimeAsync(refreshToken);
        
        if (refreshTokenLifetime == null)
            throw new Exception("Refresh token not found");
        
        if (refreshTokenLifetime < DateTime.UtcNow)
            throw new Exception("Refresh token expired");
    }

    private async Task<IList<Claim>> GetUserClaimListAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? throw new Exception("User not found");
        var roles = await _userManager.GetRolesAsync(user);
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
        };
        
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return claims;
    }
}