namespace IAC.Domain.Repositories;

public interface ITokenRepository
{
     Task AddAsync(string userId, string refreshToken, DateTime refreshTokenExpiryTime);
     Task<DateTime?> GetExpiryTimeAsync(string refreshToken);
     Task RemoveAsync(string refreshToken);
}