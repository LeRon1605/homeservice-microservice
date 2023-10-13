using IAC.Domain.Entities;

namespace IAC.Domain.Repositories;

public interface IUserRepository
{
    Task<bool> IsPhoneExist(string phone);
    
    Task<bool> IsEmailExist(string? email);
    
    Task<ApplicationUser?> GetByPhoneNumberAsync(string phoneNumber);
    
    Task<ApplicationUser?> GetByEmailAsync(string email);
    
    Task<ApplicationUser?> GetByRefreshTokenAsync(string refreshToken);

    Task<ApplicationUser?> GetByIdentifierAndRoleAsync(string identifier, string[] roleIds);
}