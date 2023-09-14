using IAC.Application.Dtos.Authentication;

namespace IAC.Application.Services.Interfaces;

public interface IAuthenticateService
{
    Task<TokenDto> LoginAsync(LoginDto logInDto);
    Task SignUpAsync(SignUpDto signUpDto);
}