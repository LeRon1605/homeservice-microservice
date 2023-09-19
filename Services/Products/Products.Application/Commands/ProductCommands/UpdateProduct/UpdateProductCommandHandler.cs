using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Products.Application.Dtos;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductAggregate.Exceptions;
using Products.Domain.ProductAggregate.Specifications;
using Products.Domain.ProductGroupAggregate;
using Products.Domain.ProductTypeAggregate;
using Products.Domain.ProductUnitAggregate;

namespace Products.Application.Commands.ProductCommands.UpdateProduct;

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, GetProductDto>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<ProductType> _productTypeRepository;
    private readonly IRepository<ProductGroup> _productGroupRepository;
    private readonly IRepository<ProductUnit> _productUnitRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateProductCommandHandler(IRepository<Product> productRepository, IUnitOfWork unitOfWork, IMapper mapper, IRepository<ProductType> productTypeRepository, IRepository<ProductGroup> productGroupRepository, IRepository<ProductUnit> productUnitRepository)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productTypeRepository = productTypeRepository;
        _productGroupRepository = productGroupRepository;
        _productUnitRepository = productUnitRepository;
    }
    
    public async Task<GetProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindAsync(new ProductIncludeImagesSpecification(request.Id));
        //var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        
        await CheckProductTypeExistAsync(request.TypeId);
        await CheckProductGroupExistAsync(request.GroupId);
        await CheckProductUnitExistAsync(request.BuyUnitId, request.SellUnitId);
        
        await product.UpdateAsync(
            request.ProductCode, request.Name, request.TypeId,
                request.GroupId, request.Description,
                request.IsObsolete, request.BuyUnitId,
                request.Buy, request.SellUnitId,
                request.Sell,
                request.Urls,
                _productRepository
            );
        
        //_productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();
        
        return _mapper.Map<GetProductDto>(product);
    }
    
    private async Task CheckProductTypeExistAsync(Guid id)
    {
        if (!await _productTypeRepository.AnyAsync(id))
        {
            throw new ProductTypeNotFoundException(id);
        }
    }

    private async Task CheckProductGroupExistAsync(Guid id)
    {
        if (!await _productGroupRepository.AnyAsync(id))
        {
            throw new ProductGroupNotFoundException(id);
        }
    }
    
    private async Task CheckProductUnitExistAsync(Guid buyUnitId, Guid sellUnitId)
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