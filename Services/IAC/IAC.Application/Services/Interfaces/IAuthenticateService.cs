using IAC.Application.Dtos.Authentication;

namespace IAC.Application.Services.Interfaces;

public interface IAuthenticateService
{
    Task SignUpAsync(SignUpDto signUpDto);
}