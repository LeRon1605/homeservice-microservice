using IAC.Domain.Entities;
using IAC.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IAC.Infrastructure.EfCore.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public async Task<bool> IsPhoneExist(string phoneNumber)
    {
        return await _userManager.Users.AnyAsync(x => x.PhoneNumber == phoneNumber);
    }

    public async Task<bool> IsEmailExist(string? email)
    {
        return await _userManager.Users.AnyAsync(x => x.PhoneNumber == email);
    }

    public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
    {
         return await _userManager.CreateAsync(user, password);
    }
}