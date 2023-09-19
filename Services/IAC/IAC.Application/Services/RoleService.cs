using AutoMapper;
using IAC.Application.Dtos.Roles;
using IAC.Application.Services.Interfaces;
using IAC.Domain.Entities;
using IAC.Domain.Exceptions.Authentication;
using IAC.Domain.Exceptions.Roles;
using IAC.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace IAC.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public RoleService(
        IRoleRepository roleRepository,
        IMapper mapper,
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<RoleDto> GetByIdAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            throw new RoleNotFoundException(id);
        }
        
        return _mapper.Map<RoleDto>(role);
    }
    
    public async Task<IEnumerable<RoleDto>> GetByUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException(userId);
        }
        
        var roles = await _roleRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }
}