using IAC.Application.Dtos.Authentication;

namespace IAC.Application.Services.Interfaces;

public interface IAuthenticateService
{
    Task SignUp(SignUpDto signUpDto);
}