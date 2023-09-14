using IAC.Domain.Entities;

namespace IAC.Domain.Repositories;

public interface IUserRepository
{
    Task<bool> IsPhoneExist(string phone);
    Task<bool> IsEmailExist(string? email);
}