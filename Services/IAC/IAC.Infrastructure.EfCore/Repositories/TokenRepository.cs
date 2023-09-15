using IAC.Domain.Entities;
using IAC.Domain.Exceptions.Authentication;
using IAC.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IAC.Infrastructure.EfCore.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly IacDbContext _context;
    
    public TokenRepository(IacDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(string userId,
                         string refreshToken,
                         DateTime refreshTokenExpiryTime)
    {
        var refreshTokenEntity = new RefreshToken
        {
            UserId = userId,
            Token = refreshToken,
            Expires = refreshTokenExpiryTime
        };
        
        _context.RefreshTokens.Add(refreshTokenEntity);
        await _context.SaveChangesAsync();

    }

    public async Task<DateTime?> GetExpiryTimeAsync(string refreshToken)
    {
        var refreshTokenEntity = await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == refreshToken);
        
        return refreshTokenEntity?.Expires; 
    }

    public async Task RemoveAsync(string refreshToken)
    {
        var refreshTokenEntity = await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Token == refreshToken)
                                 ?? throw new RefreshTokenNotFound();
        
        _context.RefreshTokens.Remove(refreshTokenEntity);

        await _context.SaveChangesAsync();
    }
}