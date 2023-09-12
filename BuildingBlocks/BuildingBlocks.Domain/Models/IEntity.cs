using BuildingBlocks.Domain.Event;

namespace BuildingBlocks.Domain.Models;

public interface IEntity
{
    Guid Id { get; }

    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    void AddDomainEvent(IDomainEvent eventItem);

    void RemoveDomainEvent(IDomainEvent eventItem);

    void ClearDomainEvents();
}