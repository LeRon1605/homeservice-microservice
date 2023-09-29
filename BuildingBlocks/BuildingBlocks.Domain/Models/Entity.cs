using BuildingBlocks.Domain.Event;
using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Models;


public abstract class Entity : IEntity
{
    public Guid Id { get; protected set; }

    private List<IDomainEvent> _domainEvents = null!;
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

    // public Entity()
    // {
    //     Id = Guid.NewGuid();
    // }
    
    public void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents ??= new List<IDomainEvent>();
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