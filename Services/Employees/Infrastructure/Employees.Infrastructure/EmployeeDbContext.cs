using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Employees.Infrastructure;

public class EmployeeDbContext : AppDbContextBase
{
    public EmployeeDbContext(DbContextOptions options,
        ILogger<AppDbContextBase> logger, IMediator mediator,
        ICurrentUser currentUser) : base(options, logger, mediator, currentUser)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeDbContext).Assembly);
    }
}