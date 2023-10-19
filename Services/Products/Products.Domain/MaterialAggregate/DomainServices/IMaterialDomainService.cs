namespace Products.Domain.MaterialAggregate.DomainServices;

public interface IMaterialDomainService
{
    Task<Material> CreateAsync(
        string materialCode,
        string name,
        Guid productTypeId,
        Guid? sellUnitId,
        decimal sellPrice,
        decimal? cost,
        bool isObsolete);
    
    Task UpdateAsync(
        Material material,
        string materialCode,
        string name,
        Guid productTypeId,
        Guid? sellUnitId,
        decimal sellPrice,
        decimal? cost,
        bool isObsolete);
}