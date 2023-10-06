using IAC.Application.Dtos.Authentication;
using IAC.Application.Dtos.Users;
using IAC.Domain.Enums;

namespace IAC.Application.Services.Interfaces;

public interface IAuthenticateService
{
    Task<TokenDto> LoginAsync(LoginDto logInDto, LoginPortal loginPortal);
    Task SignUpAsync(SignUpDto signUpDto);
    Task<TokenDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
    Task<UserDto> GetCurrentUserInfoAsync();
}