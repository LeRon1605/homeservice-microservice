using IAC.Application.Services.Interfaces;
using IAC.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IAC.Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
}