﻿using System.Security.Claims;
using BuildingBlocks.Application.Identity;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Presentation.Authorization;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? Id => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? Name => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
    public string? Email => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
    public string? FullName => _httpContextAccessor.HttpContext?.User.FindFirstValue("FullName");
    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    public ClaimsPrincipal? ClaimPrincipal => _httpContextAccessor.HttpContext?.User;
    
    public bool IsInRole(string role)
    {
        return _httpContextAccessor.HttpContext?.User.IsInRole(role) ?? false;
    }

    public string? GetClaim(string claimType)
    {
        return _httpContextAccessor.HttpContext?.User.FindFirstValue(claimType);
    }
}