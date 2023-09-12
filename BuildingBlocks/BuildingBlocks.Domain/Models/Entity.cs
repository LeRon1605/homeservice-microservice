using BuildingBlocks.Domain.Event;

namespace BuildingBlocks.Domain.Models;


public abstract class Entity : IEntity
{
    public Guid Id { get; }
    
    private List<IDomainEvent> _domainEvents;
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    protected Entity()
    {
        Id = Guid.NewGuid();
        
    }
    
    public void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents = _domainEvents ?? new List<IDomainEvent>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
}