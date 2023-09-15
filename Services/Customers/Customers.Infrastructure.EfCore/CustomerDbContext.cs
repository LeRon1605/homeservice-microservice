using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Customers.Infrastructure.EfCore;

public class CustomerDbContext : AppDbContextBase
{
    public CustomerDbContext(
        DbContextOptions options, 
        ILogger<AppDbContextBase> logger, 
        IMediator mediator, 
        ICurrentUser currentUser) : base(options, logger, mediator, currentUser)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerDbContext).Assembly);
    }
}