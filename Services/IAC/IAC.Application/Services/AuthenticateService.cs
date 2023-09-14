using IAC.Application.Dtos.Authentication;
using IAC.Application.Services.Interfaces;
using IAC.Domain.Entities;
using IAC.Domain.Exceptions.Authentication;
using IAC.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace IAC.Application.Services;

public class AuthenticateService : IAuthenticateService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthenticateService(IUserRepository userRepository,
                               ITokenService tokenService,
                               UserManager<ApplicationUser> userManager)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _userManager = userManager;
    }
    public async Task SignUp(SignUpDto signUpDto)
    {
        bool userExist = await _userRepository.IsPhoneExist(signUpDto.PhoneNumber)
            && await _userRepository.IsEmailExist(signUpDto.Email);
    }
    
    public async Task<TokenDto> LoginAsync(LoginDto logInDto)
    {
        var user = await _userRepository.GetByPhoneNumberAsync(logInDto.Identifier) 
                   ?? await _userRepository.GetByEmailAsync(logInDto.Identifier);
        
        if (user == null)
            throw new UserNotFoundException(logInDto.Identifier);
        
        var isValid = await _userManager.CheckPasswordAsync(user, logInDto.Password);
        if (!isValid)
            throw new InvalidPasswordException();

        var tokenDto = new TokenDto
        {
            AccessToken = await _tokenService.GenerateAccessTokenAsync(user.Id),
            RefreshToken = _tokenService.GenerateRefreshToken()
        };
        
        await _tokenService.AddRefreshTokenAsync(user.Id, tokenDto.RefreshToken);
        
        return tokenDto;
    } 
}