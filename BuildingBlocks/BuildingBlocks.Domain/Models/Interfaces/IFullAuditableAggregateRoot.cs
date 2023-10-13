namespace BuildingBlocks.Domain.Models.Interfaces;

public interface IFullAuditableAggregateRoot : IAuditableAggregateRoot, IHasSoftDeleteAggregateRoot
{
    
}