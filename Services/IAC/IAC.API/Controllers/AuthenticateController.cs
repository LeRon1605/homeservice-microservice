﻿using IAC.Application.Dtos.Authentication;
using IAC.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IAC.API.Controllers
{
	[Route("api/users")]
	[ApiController]
	public class AuthenticateController : ControllerBase
	{
		private readonly IAuthenticateService _authenticateService;
        public AuthenticateController(IAuthenticateService authenticateService)
        {
	        _authenticateService = authenticateService;
        }

        // [HttpPost("sign-up")]
        // public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        // {
	       //  var result = await _authenticateService.SignUp(signUpDto);
	       //  return Ok(result);
        // }
    }
}
