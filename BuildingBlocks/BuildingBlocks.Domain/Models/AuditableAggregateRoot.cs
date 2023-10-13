using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Models;

public abstract class AuditableAggregateRoot : AggregateRoot, IAuditableAggregateRoot
{
    public DateTime CreatedAt { get; set; }
    public string? CreatedByUserId { get; set; }
    public string? CreatedBy { get; set; }
    
    public DateTime? LastModifiedAt { get; set; }
    public string? LastModifiedByUserId { get; set; }
    public string? LastModifiedBy { get; set; }
}