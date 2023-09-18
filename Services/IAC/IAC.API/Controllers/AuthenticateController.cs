using IAC.Application.Dtos.Authentication;
using IAC.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IAC.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticateController : ControllerBase
{
	private readonly IAuthenticateService _authenticateService;
	public AuthenticateController(IAuthenticateService authenticateService)
	{
		_authenticateService = authenticateService;
	}

	[HttpPost("sign-up")]
	public async Task<IActionResult> SignUpAsync([FromBody] SignUpDto signUpDto)
	{
		await _authenticateService.SignUpAsync(signUpDto);
		return Ok();
	}
        
	[HttpPost("log-in")]
	public async Task<IActionResult> Login([FromBody] LoginDto logInDto)
	{
		var result = await _authenticateService.LoginAsync(logInDto);
		return Ok(result);
	}
        
	[HttpPost("refresh-token")]
	public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
	{
		var result = await _authenticateService.RefreshTokenAsync(refreshTokenDto);
		return Ok(result);
	}
}