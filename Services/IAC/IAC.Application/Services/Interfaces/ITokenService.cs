
namespace IAC.Application.Services.Interfaces;

public interface ITokenService
{
    string GenerateRefreshToken();
    
    Task<string> GenerateAccessTokenAsync(string userId);

    Task AddRefreshTokenAsync(string userId, string refreshToken);

    Task ValidateRefreshTokenAsync(string refreshToken);
    
    Task RevokeRefreshTokenAsync(string refreshToken);
}