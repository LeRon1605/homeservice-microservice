using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EfCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Products.Domain.ProductAggregate;

namespace Products.Infrastructure.Data;

public class ProductDbContext : AppDbContextBase
{

    public DbSet<Product> Products { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

    public ProductDbContext(DbContextOptions options,
             ILogger<AppDbContextBase> logger,
             IMediator mediator,
             ICurrentUser currentUser) : base(options, logger, mediator, currentUser)
    {
    }
}