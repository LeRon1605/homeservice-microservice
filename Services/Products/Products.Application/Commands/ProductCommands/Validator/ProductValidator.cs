using BuildingBlocks.Domain.Data;
using Products.Domain.ProductAggregate.Exceptions;
using Products.Domain.ProductGroupAggregate;
using Products.Domain.ProductTypeAggregate;
using Products.Domain.ProductUnitAggregate;

namespace Products.Application.Commands.ProductCommands.Validator;

public class ProductValidator : IProductValidator
{
    private readonly IRepository<ProductType> _productTypeRepository;
    private readonly IRepository<ProductGroup> _productGroupRepository;
    private readonly IRepository<ProductUnit> _productUnitRepository;
    
    public ProductValidator(IRepository<ProductType> productTypeRepository,
        IRepository<ProductGroup> productGroupRepository,
        IRepository<ProductUnit> productUnitRepository)
    {
        _productTypeRepository = productTypeRepository;
        _productGroupRepository = productGroupRepository;
        _productUnitRepository = productUnitRepository;
    }


    public async Task CheckProductTypeExistAsync(Guid id)
    {
        if (!await _productTypeRepository.AnyAsync(id))
        {
            throw new ProductTypeNotFoundException(id);
        }
    }

    public async Task CheckProductGroupExistAsync(Guid id)
    {
        if (!await _productGroupRepository.AnyAsync(id))
        {
            throw new ProductGroupNotFoundException(id);
        }
    }

    public async Task CheckProductUnitExistAsync(Guid buyUnitId, Guid sellUnitId)
    {
        if (!await _productUnitRepository.AnyAsync(buyUnitId))
        {
            throw new ProductUnitNotFoundException(buyUnitId);
        }

        if (!await _productUnitRepository.AnyAsync(sellUnitId))
        {
            throw new ProductUnitNotFoundException(sellUnitId);
        }
    }
}