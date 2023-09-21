using IAC.Application.Dtos.Users;

namespace IAC.Application.Services.Interfaces;

public interface IUserService
{
    Task UpdateUserInfoAsync(UserInfoDto userInfoDto);
    
    Task DeleteUserAsync(Guid userId);

    Task<bool> AnyAsync(Guid userId);
}