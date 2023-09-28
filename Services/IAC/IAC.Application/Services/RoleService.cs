using AutoMapper;
using BuildingBlocks.Application.Cache;
using BuildingBlocks.Domain.Exceptions.Common;
using BuildingBlocks.Domain.Exceptions.Resource;
using IAC.Application.Auth;
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
    private readonly ICacheService _cacheService;

    private const int PermissionCacheInMinutes = 5;
    
    public RoleService(
        IRoleRepository roleRepository,
        IMapper mapper,
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        ICacheService cacheService)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
        _roleManager = roleManager;
        _userManager = userManager;
        _cacheService = cacheService;
    }

    public IEnumerable<PermissionInfo> GetAllPermissions()
    {
        return PermissionPolicy.AllPermissions;
    }

    public async Task<IEnumerable<string>> GetPermissionsInRoleAsync(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId)
                   ?? throw new RoleNotFoundException(roleId);
        
        var permissions = (await _roleManager.GetClaimsAsync(role)).Select(c => c.Value).ToList();
        
        var key = KeyGenerator.Generate(CacheType.Permission, role.Name!);
        var cachedPermissions = await _cacheService.GetCachedDataAsync<IEnumerable<string>>(key);
        if (cachedPermissions is null)
        {
            await _cacheService.SetCachedDataAsync(key, permissions, TimeSpan.FromMinutes(PermissionCacheInMinutes));
        } 
        
        return permissions;
    }

    public async Task EditPermissionsInRoleAsync(string roleId, IList<string> permissions)
    {
        CheckPermissionConstraints(permissions);
        
        var role = await _roleRepository.GetByIdAsync(roleId) ?? throw new RoleNotFoundException(roleId);
        
        // Replace permission list for a role
        var allPermission = PermissionPolicy.AllPermissions.Select(p => p.Code).ToList();
        role.RoleClaims.Clear();
        foreach (var permission in permissions)
        {
            if (!allPermission.Contains(permission))
                throw new PermissionNotFoundException(permission);

            role.RoleClaims.Add(new IdentityRoleClaim<string>
            {
                RoleId = roleId,
                ClaimType = "Permission",
                ClaimValue = permission 
            });
        }

        // Update to db
        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded)
            throw new InvalidInputException(result.Errors.First().Description);
        
        // Override cache
        var key = KeyGenerator.Generate(CacheType.Permission, role.Name!);
        await _cacheService.SetCachedDataAsync(key, permissions, TimeSpan.FromMinutes(PermissionCacheInMinutes));
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

    private void CheckPermissionConstraints(IList<string> permissions)
    {
        var allModules = typeof(PermissionPolicy).GetNestedTypes().Where(x => x.IsClass).Select(x => x.Name).ToList();

        foreach (var moduleName in allModules)
        {
            if (moduleName == nameof(PermissionPolicy.Common)) continue; 
            
            // If user has access to a module, there must be a View permission
            if (permissions.Any(p => p.Contains(moduleName)) && !permissions.Any(p => p.Contains($"{moduleName}.View")))
                throw new PermissionConstraintsNotSatisfiedException(moduleName);
        }
    }
    
}