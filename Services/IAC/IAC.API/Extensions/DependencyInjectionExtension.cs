using BuildingBlocks.Application.Seeder;
using IAC.Application;
using IAC.Application.Seeder;
using IAC.Application.Services;
using IAC.Application.Services.Interfaces;
using IAC.Domain.Entities;
using IAC.Domain.Repositories;
using IAC.Infrastructure.EfCore;
using IAC.Infrastructure.EfCore.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IAC.API.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddDbContext<IacDbContext>(options =>
        {
            options.EnableSensitiveDataLogging(env.IsDevelopment());
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });
        
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
    
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddScoped<IDataSeeder, IdentityDataSeeder>();
        // services.AddScoped<IDataSeeder, IdentityDataSeeder>();
        
        services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<IacDbContext>()
                .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;

            // Signin settings.
            options.SignIn.RequireConfirmedEmail = false;
        });

        return services;
    }
    
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(IdentityApplicationReferenceAssembly));
        
        return services;
    }
}