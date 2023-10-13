using BuildingBlocks.Domain.Models.Interfaces;

namespace BuildingBlocks.Domain.Models;

public abstract class FullAuditableEntity : AuditableEntity, IFullAuditableEntity
{
    public bool IsDeleted { get; set; }
    public string? DeletedByUserId { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
}