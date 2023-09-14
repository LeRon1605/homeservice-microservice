using IAC.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IAC.Domain.Repositories;

public interface IUserRepository
{
    Task<bool> IsPhoneExist(string phone);
    Task<bool> IsEmailExist(string? email);
    Task<ApplicationUser?> GetByPhoneNumberAsync(string phoneNumber);
    Task<ApplicationUser?> GetByEmailAsync(string email);
}