using BuildingBlocks.Domain.Data;
using IAC.Application.Dtos.Authentication;
using IAC.Application.Services.Interfaces;
using IAC.Domain.Entities;
using IAC.Domain.Exceptions.Users;
using IAC.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace IAC.Application.Services;

public class AuthenticateService : IAuthenticateService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    public AuthenticateService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }
    public async Task SignUpAsync(SignUpDto signUpDto)
    {
        bool isUserExist = await _userRepository.IsPhoneExist(signUpDto.PhoneNumber)
            && await _userRepository.IsEmailExist(signUpDto.Email);
        if (isUserExist)
            throw new UserExistException("User is already exist");
        var user = new ApplicationUser()
        {
            FirstName = signUpDto.FirstName,
            LastName = signUpDto.LastName,
            UserName = signUpDto.Email,
            PhoneNumber = signUpDto.PhoneNumber,
            Email = signUpDto.Email,
            PasswordHash = signUpDto.Password
        };
        //TO DO: Create Role for admin & user

        var result = await _userManager.CreateAsync(user, signUpDto.Password);
        if (!result.Succeeded)
            throw new UserCreateFailException("Can not create user");
    }
}