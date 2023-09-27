using IAC.Domain.Entities;
using IAC.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IAC.Infrastructure.EfCore.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly IacDbContext _dbContext;

    public RoleRepository(IacDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<ApplicationRole>> GetAllAsync()
    {
        return await _dbContext.Roles.ToListAsync();
    }

    public async Task<IList<ApplicationRole>> GetByUserIdAsync(string userId)
    {
        var userInRoleQueryable = _dbContext.UserRoles.Where(x => x.UserId == userId);
        return await _dbContext.Roles
                               .Where(role => userInRoleQueryable.Any(x => x.RoleId == role.Id))
                               .ToListAsync();
    }

    public Task<bool> HasGrantedToUserAsync(string id)
    {
        return _dbContext.UserRoles.AnyAsync(x => x.RoleId == id);
    }
}