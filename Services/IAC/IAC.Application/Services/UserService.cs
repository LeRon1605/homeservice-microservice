using IAC.Application.Dtos.Users;
using IAC.Application.Services.Interfaces;
using IAC.Domain.Entities;
using IAC.Domain.Exceptions.Authentication;
using Microsoft.AspNetCore.Identity;

namespace IAC.Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task UpdateUserInfoAsync(UserInfoDto userInfoDto)
    {
        var user = await _userManager.FindByIdAsync(userInfoDto.Id.ToString());

        if (user is null)
        {
            throw new UserNotFoundException(userInfoDto.Id);
        }

        user.FullName = userInfoDto.FullName;
        user.PhoneNumber = userInfoDto.Phone;
        user.Email = userInfoDto.Email;

        await _userManager.UpdateAsync(user); 
    }

    public async Task<bool> AnyAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        return user is not null;
    }
}