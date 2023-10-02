using IAC.Application.Dtos.Authentication;
using IAC.Application.Services.Interfaces;
using IAC.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace IAC.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthenticateController : ControllerBase
{
	private readonly IAuthenticateService _authenticateService;
	private readonly ITokenService _tokenService;
	public AuthenticateController(
		IAuthenticateService authenticateService,
		ITokenService tokenService
		)
	{
		_authenticateService = authenticateService;
		_tokenService = tokenService;
	}

	[HttpPost("customer/sign-up")]
	public async Task<IActionResult> SignUpAsync([FromBody] SignUpDto signUpDto)
	{
		await _authenticateService.SignUpAsync(signUpDto);
		return Ok();
	}
        
	[HttpPost("backoffice/log-in")]
	public async Task<IActionResult> BackofficeLogin([FromBody] LoginDto logInDto)
	{
		var result = await _authenticateService.LoginAsync(logInDto, LoginPortal.BackOffice);
		return Ok(result);
	}
	
	[HttpPost("customer/log-in")]
	public async Task<IActionResult> CustomerLogin([FromBody] LoginDto logInDto)
	{
		var result = await _authenticateService.LoginAsync(logInDto, LoginPortal.Customer);
		return Ok(result);
	}
	
	[HttpPost("refresh-token")]
	public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
	{
		var result = await _authenticateService.RefreshTokenAsync(refreshTokenDto);
		return Ok(result);
	}
	
	[HttpPost("refresh-token/revoke")]
	public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
	{
		await _tokenService.RevokeRefreshTokenAsync(refreshTokenDto.RefreshToken);
		return Ok();
	}
}