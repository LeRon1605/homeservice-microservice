using BuildingBlocks.Domain.Data;
using IAC.Application.Dtos.Authentication;
using IAC.Application.Services.Interfaces;
using IAC.Domain.Entities;
using IAC.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace IAC.Application.Services;

public class AuthenticateService : IAuthenticateService
{
    private readonly IUserRepository _userRepository;

    public AuthenticateService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task SignUp(SignUpDto signUpDto)
    {
        bool userExist = await _userRepository.IsPhoneExist(signUpDto.PhoneNumber)
            && await _userRepository.IsEmailExist(signUpDto.Email);
    }
}