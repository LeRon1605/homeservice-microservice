using BuildingBlocks.Domain.Data;
using Products.Domain.MaterialAggregate.Exceptions;
using Products.Domain.MaterialAggregate.Specifications;
using Products.Domain.ProductTypeAggregate;
using Products.Domain.ProductTypeAggregate.Exceptions;
using Products.Domain.ProductUnitAggregate;
using Products.Domain.ProductUnitAggregate.Exceptions;

namespace Products.Domain.MaterialAggregate.DomainServices;

public class MaterialDomainService : IMaterialDomainService
{
    private readonly IRepository<Material> _materialRepository;
    private readonly IRepository<ProductType> _productTypeRepository;
    private readonly IRepository<ProductUnit> _productUnitRepository;

    public MaterialDomainService(
        IRepository<Material> repository,
        IRepository<ProductType> productTypeRepository,
        IRepository<ProductUnit> productUnitRepository)
    {
        _materialRepository = repository;
        _productTypeRepository = productTypeRepository;
        _productUnitRepository = productUnitRepository;
    }

    public async Task<Material> CreateAsync(
        string materialCode, 
        string name, 
        Guid productTypeId, 
        Guid? sellUnitId, 
        decimal? sellPrice,
        decimal? cost, 
        bool isObsolete)
    {
        await CheckProductTypeExisted(productTypeId);
        await CheckSellUnitExisted(sellUnitId);
        await CheckMaterialCodeDuplicated(materialCode);
        
        return new Material(materialCode, name, productTypeId, sellUnitId, sellPrice, cost, isObsolete);
    }

    public async Task UpdateAsync(
        Material material, 
        string materialCode, 
        string name, 
        Guid productTypeId, 
        Guid? sellUnitId,
        decimal? sellPrice, 
        decimal? cost, 
        bool isObsolete)
    {
        if (sellUnitId.HasValue && material.SellUnitId != sellUnitId.Value)
        {
            await CheckSellUnitExisted(sellUnitId);
            material.SetSellUnit(sellUnitId);
        }
        
        if (material.MaterialCode != materialCode)
        {
            await CheckMaterialCodeDuplicated(materialCode);
            material.SetMaterialCode(materialCode);
        }
        
        if (material.ProductTypeId != productTypeId)
        {
            await CheckProductTypeExisted(productTypeId);
            material.SetProductType(productTypeId);
        }
        
        material.SetName(name);
        material.SetSellPrice(sellPrice);
        material.SetCost(cost);
        material.IsObsoleteYet(isObsolete);
    }

    private async Task CheckProductTypeExisted(Guid productTypeId)
    {
        var isProductTypeExisted = await _productTypeRepository.AnyAsync(productTypeId);

        if (!isProductTypeExisted)
        {
            throw new ProductTypeNotFoundException(productTypeId);
        }
    }
    
    private async Task CheckMaterialCodeDuplicated(string materialCode)
    {
        var isMaterialCodeDuplicated = await _materialRepository.AnyAsync(
            new MaterialCodeDuplicatedSpecification(materialCode));

        if (isMaterialCodeDuplicated)
        {
            throw new MaterialCodeExistedException(materialCode);
        }
    }
    
    private async Task CheckSellUnitExisted(Guid? sellUnitId)
    {
        if (sellUnitId is null)
        {
            return;    
        }
        
        var isSellUnitExisted = await _productUnitRepository.AnyAsync(sellUnitId.Value);

        if (!isSellUnitExisted)
        {
            throw new ProductUnitNotFoundException(sellUnitId.Value);   
        }
    }
}