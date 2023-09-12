using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Models.Interfaces;
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
        
        modelBuilder.Entity<IAuditableEntity>().HasQueryFilter(s => !s.IsDeleted);
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
            .Entries<IEntity>()
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
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUser.Id;
                    entry.Entity.CreatedAt = DateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUser.Id;
                    entry.Entity.LastModifiedAt = DateTime.Now;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.LastModifiedAt = DateTime.Now;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedBy = _currentUser.Id;
                    break;
            }
        }
    }
}