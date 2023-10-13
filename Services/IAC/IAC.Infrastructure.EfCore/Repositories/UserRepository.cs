using IAC.Domain.Entities;
using IAC.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IAC.Infrastructure.EfCore.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IacDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(UserManager<ApplicationUser> userManager, IacDbContext dbContext)
    {
        _userManager = userManager;
        _dbContext = dbContext;
    }
    public async Task<bool> IsPhoneExist(string phoneNumber)
    {
        return await _userManager.Users.AnyAsync(x => x.PhoneNumber == phoneNumber);
    }

    public async Task<bool> IsEmailExist(string? email)
    {
        return await _userManager.Users.AnyAsync(x => x.PhoneNumber == email);
    }

    public async Task<ApplicationUser?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }

    public async Task<ApplicationUser?> GetByEmailAsync(string email)
    {
        return await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<ApplicationUser?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshTokens.Any(y => y.Token == refreshToken));
    }

    public async Task<ApplicationUser?> GetByIdentifierAndRoleAsync(string identifier, string[] roleIds)
    {
        var userInRoleQueryable = _dbContext.UserRoles.Where(x => roleIds.Contains(x.RoleId));
        return await _dbContext.Users
            .Where(x => x.PhoneNumber == identifier || x.NormalizedEmail == identifier.ToUpper())
            .FirstOrDefaultAsync(x => userInRoleQueryable.Any(userInRole => userInRole.UserId == x.Id));
    }
}