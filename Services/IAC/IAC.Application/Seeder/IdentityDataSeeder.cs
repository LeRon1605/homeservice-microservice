using BuildingBlocks.Application.Seeder;
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

    public IdentityDataSeeder(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ILogger<IdentityDataSeeder> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }
    
    public async Task SeedAsync()
    {
        try
        {
            _logger.LogInformation("Begin seeding identity data...");
            
            await SeedAdminRoleAsync();

            await SeedCustomerRoleAsync();

            if (!_userManager.Users.Any())
            {
                await SeedDefaultAdminAccountAsync();
            }
            
            _logger.LogInformation("Seed identity data successfully!");
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Seeding identity data failed!", ex);
        }
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
    
    private async Task SeedDefaultAdminAccountAsync()
    {
        var user = new ApplicationUser()
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            FirstName = "Admin",
            LastName = "Admin",
            PhoneNumber = "0123456789",
            SecurityStamp = Guid.NewGuid().ToString(),
            EmailConfirmed = true
        };

        await _userManager.CreateAsync(user, "Admin@123");
        await _userManager.AddToRoleAsync(user, AppRole.Admin);
    }
}