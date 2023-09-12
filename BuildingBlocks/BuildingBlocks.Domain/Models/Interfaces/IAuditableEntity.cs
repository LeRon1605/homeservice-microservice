﻿namespace BuildingBlocks.Domain.Models.Interfaces;

public interface IAuditableEntity : IEntity
{
    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime LastModifiedAt { get; set; }

    public string? LastModifiedBy { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public string? DeletedBy { get; set; }
}