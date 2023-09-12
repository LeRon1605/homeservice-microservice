using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Models;

public abstract class AuditableAggregateRoot : AuditableEntity, IAuditableAggregateRoot
{
    
}