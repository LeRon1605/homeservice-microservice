namespace BuildingBlocks.Domain.Models.Interfaces;

public interface IHasSoftDeleteEntity : IEntity
{
    public bool IsDeleted { get; set; }
    
    public string? DeletedByUserId { get; set; }
    
    public string? DeletedBy { get; set; }
    
    public DateTime? DeletedAt { get; set; }
}