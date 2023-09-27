using AutoMapper;
using BuildingBlocks.Domain.Exceptions.Resource;
using IAC.Application.Dtos.Roles;
using IAC.Application.Services.Interfaces;
using IAC.Domain.Constants;
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

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }

    public async Task<RoleDto> GetByIdAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id) ?? throw new RoleNotFoundException(id);
        
        return _mapper.Map<RoleDto>(role);
    }

    public async Task<RoleDto> CreateAsync(RoleCreateDto dto)
    {
        var role = new ApplicationRole(dto.Name);
        var result = await _roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);
        }

        return _mapper.Map<RoleDto>(role);
    }

    public async Task<RoleDto> UpdateAsync(string id, RoleUpdateDto dto)
    {
        var role = await _roleManager.FindByIdAsync(id) ?? throw new RoleNotFoundException(id);

        role.Name = dto.Name;
        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);
        }

        return _mapper.Map<RoleDto>(role);
    }

    public async Task DeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id) ?? throw new RoleNotFoundException(id);

        if (IsDefaultRole(role))
        {
            throw new DefaultRoleDeleteFailedException(role.Name!);
        }
        
        if (await _roleRepository.HasGrantedToUserAsync(id))
        {
            throw new RoleHasGrantedToUserException(id);
        }
        
        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            var error = result.Errors.First();
            throw new ResourceInvalidOperationException(error.Description, error.Code);
        }
    }

    public async Task<IEnumerable<RoleDto>> GetByUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new UserNotFoundException(nameof(ApplicationUser.Id), userId);
        }
        
        var roles = await _roleRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }

    private bool IsDefaultRole(ApplicationRole role)
    {
        return role.Name == AppRole.Admin || role.Name == AppRole.Customer;
    }
}