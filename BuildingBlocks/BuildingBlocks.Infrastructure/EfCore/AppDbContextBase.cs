using System.Linq.Expressions;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Infrastructure.EfCore;

public abstract class AppDbContextBase : DbContext
{
    private readonly ILogger<AppDbContextBase> _logger;
    private readonly IMediator _mediator;
    private readonly ICurrentUser _currentUser;
    protected AppDbContextBase(
        DbContextOptions options, 
        ILogger<AppDbContextBase> logger, 
        IMediator mediator,
        ICurrentUser currentUser) : base(options)
    {
        _logger = logger;
        _mediator = mediator;
        _currentUser = currentUser;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "p");
                var deletedCheck =
                    Expression.Lambda(
                        Expression.Equal(Expression.Property(parameter, nameof(AuditableEntity.IsDeleted)), 
                        Expression.Constant(false)),
                        parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(deletedCheck);
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        ProcessAuditEntityState();
        
        await DispatchDomainEventsAsync();
        
        return await base.SaveChangesAsync(cancellationToken);
    }

    private async Task DispatchDomainEventsAsync()
    {
        var domainEntities = ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            _logger.LogInformation("Dispatching domain event. Event {eventName}: {eventId}", domainEvent.GetType().Name, domainEvent.Id);
            await _mediator.Publish(domainEvent);   
        }
    }

    private void ProcessAuditEntityState()
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedByUserId = _currentUser.Id;
                    entry.Entity.CreatedBy = _currentUser.FullName;
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.IsDeleted = false;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedByUserId = _currentUser.Id;
                    entry.Entity.LastModifiedBy = _currentUser.FullName;
                    entry.Entity.LastModifiedAt = DateTime.Now;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.LastModifiedAt = DateTime.Now;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedByUserId = _currentUser.Id;
                    entry.Entity.DeletedBy = _currentUser.FullName;
                    break;
            }
        }
    }
}