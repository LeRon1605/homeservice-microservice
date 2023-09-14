using IAC.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IAC.Infrastructure.EfCore;

public class IacDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public IacDbContext(DbContextOptions<IacDbContext> options) : base(options)
    {
        
    }

	protected override void OnModelCreating(ModelBuilder builder)
	{

		base.OnModelCreating(builder);
		foreach (var entityType in builder.Model.GetEntityTypes())
		{
			var tableName = entityType.GetTableName();
			if (tableName.StartsWith("AspNet"))
			{
				entityType.SetTableName(tableName.Substring(6));
			}
		}
	}
}