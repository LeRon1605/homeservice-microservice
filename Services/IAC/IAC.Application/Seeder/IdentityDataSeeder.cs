using System.Security.Claims;
using BuildingBlocks.Application.Seeder;
using BuildingBlocks.EventBus.Interfaces;
using IAC.Application.Auth;
using IAC.Application.IntegrationEvents.Events;
using IAC.Domain.Constants;
using IAC.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IAC.Application.Seeder;

public class IdentityDataSeeder : IDataSeeder
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<IdentityDataSeeder> _logger;
    private readonly IEventBus _eventBus;

    public IdentityDataSeeder(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ILogger<IdentityDataSeeder> logger,
        IEventBus eventBus)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _eventBus = eventBus;
    }
    
    public async Task SeedAsync()
    {
        try
        {
            _logger.LogInformation("Begin seeding identity data...");

            if (!_roleManager.Roles.Any())
            {
                await SeedAdminRoleAsync();

                await SeedCustomerRoleAsync();

                await SeedSalePersonRoleAsync();
                
                await SeedRoleClaimAsync();
            }

            if (!_userManager.Users.Any())
            {
                await SeedDefaultAdminAccountAsync();
                await SeedCustomerUserAccountAsync();
            }
            
            _logger.LogInformation("Seed identity data successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Seeding identity data failed!", ex);
        }
    }

    private async Task SeedCustomerUserAccountAsync()
    {
        var user = new ApplicationUser()
        {
            UserName = "user",
            Email = "user@gmail.com",
            FullName = "User",
            PhoneNumber = "0123456788",
            SecurityStamp = Guid.NewGuid().ToString(),
            EmailConfirmed = true
        };

        await _userManager.CreateAsync(user, "User@123");
        await _userManager.AddToRoleAsync(user, AppRole.Customer);
        
        _eventBus.Publish(new UserSignedUpIntegrationEvent(Guid.Parse(user.Id), user.FullName, user.Email, user.PhoneNumber));
    }

    private async Task SeedAdminRoleAsync()
    {
        var adminRole = new ApplicationRole(AppRole.Admin);
        var result = await _roleManager.CreateAsync(adminRole);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Seeding admin role failed!");
        }
    }

    private async Task SeedCustomerRoleAsync()
    {
        var customerRole = new ApplicationRole(AppRole.Customer);
        var result = await _roleManager.CreateAsync(customerRole);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Seeding customer role failed!");
        }
    }

    private async Task SeedSalePersonRoleAsync()
    {
        var salePersonRole = new ApplicationRole(AppRole.SalePerson);
        var result = await _roleManager.CreateAsync(salePersonRole);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Seeding sale person role failed!");       
        }
    }
    
    
    private async Task SeedDefaultAdminAccountAsync()
    {
        var user = new ApplicationUser()
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            FullName = "Admin",
            PhoneNumber = "0123456789",
            SecurityStamp = Guid.NewGuid().ToString(),
            EmailConfirmed = true
        };

        await _userManager.CreateAsync(user, "Admin@123");
        await _userManager.AddToRoleAsync(user, AppRole.Admin);
    }

    private async Task SeedRoleClaimAsync()
    {
        var permissions = PermissionPolicy.AllPermissions;
        var adminRole = await _roleManager.FindByNameAsync(AppRole.Admin);
        var salePersonRole = await _roleManager.FindByNameAsync(AppRole.SalePerson);
        foreach (var permission in permissions)
        {
            await _roleManager.AddClaimAsync(adminRole!, new Claim("Permission", permission.Code));
            await _roleManager.AddClaimAsync(salePersonRole!, new Claim("Permission", permission.Code));
        }
    }
}