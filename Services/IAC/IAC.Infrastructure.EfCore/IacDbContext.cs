using IAC.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IAC.Infrastructure.EfCore;

public class IacDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public IacDbContext(DbContextOptions<IacDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfigurationsFromAssembly(typeof(IacDbContext).Assembly);
    }
}