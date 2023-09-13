using IAC.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IAC.Infrastructure.EfCore;

public class IacDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public IacDbContext(DbContextOptions<IacDbContext> options) : base(options)
    {
        
    }
}