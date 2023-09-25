using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Shopping.Infrastructure.EfCore;

public class OrderDbContext : AppDbContextBase
{
    public OrderDbContext(
        DbContextOptions options, 
        ILogger<AppDbContextBase> logger, 
        IMediator mediator,
        ICurrentUser currentUser) : base(options, logger, mediator, currentUser)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
    }
}