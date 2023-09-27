using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Contracts.Infrastructure.EfCore;

public class ContractDbContext : AppDbContextBase
{
    public ContractDbContext(
        DbContextOptions options, 
        ILogger<AppDbContextBase> logger, 
        IMediator mediator, 
        ICurrentUser currentUser) : base(options, logger, mediator, currentUser)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ContractDbContext).Assembly);
    }
}