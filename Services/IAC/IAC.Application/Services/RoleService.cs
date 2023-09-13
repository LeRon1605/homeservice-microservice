using AutoMapper;
using IAC.Application.Dtos.Roles;
using IAC.Application.Services.Interfaces;
using IAC.Domain.Entities;
using IAC.Domain.Exceptions.Roles;
using IAC.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace IAC.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;
    private readonly RoleManager<ApplicationRole> _roleManager;
    
    public RoleService(
        IRoleRepository roleRepository,
        IMapper mapper,
        RoleManager<ApplicationRole> roleManager)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
        _roleManager = roleManager;
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
}