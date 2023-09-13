using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using Customers.Domain.CustomerAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Customers.Infrastructure.EfCore;

public class CustomerDbContext : AppDbContextBase
{
    public CustomerDbContext(DbContextOptions options, ILogger<AppDbContextBase> logger, IMediator mediator, ICurrentUser currentUser) : base(options, logger, mediator, currentUser)
    {
    }
    
    public DbSet<Customer> Customers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerDbContext).Assembly);
        modelBuilder.SeedData();
    }
}