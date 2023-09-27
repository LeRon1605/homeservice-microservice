using IAC.Domain.Entities;

namespace IAC.Domain.Repositories;

public interface IRoleRepository
{
    Task<IList<ApplicationRole>> GetAllAsync();
    
    Task<IList<ApplicationRole>> GetByUserIdAsync(string userId);

    Task<bool> HasGrantedToUserAsync(string id);
}