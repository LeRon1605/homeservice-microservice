using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Products.Application.Dtos;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductAggregate.Specifications;
using Products.Domain.ProductAggregate.Exceptions;
using Products.Domain.ProductAggregate.Specifications;
using Products.Domain.ProductGroupAggregate;
using Products.Domain.ProductTypeAggregate;
using Products.Domain.ProductUnitAggregate;

namespace Products.Application.Commands.ProductCommands.AddProduct;

public class AddProductCommandHandler : ICommandHandler<AddProductCommand, GetProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<ProductType> _productTypeRepository;
    private readonly IRepository<ProductGroup> _productGroupRepository;
    private readonly IRepository<ProductUnit> _productUnitRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AddProductCommandHandler> _logger;
   

    public AddProductCommandHandler(IUnitOfWork unitOfWork,
        IRepository<Product> productRepository,
        IRepository<ProductType> productTypeRepository,
        IRepository<ProductGroup> productGroupRepository,
        IRepository<ProductUnit> productUnitRepository,
        IMapper mapper,
        ILogger<AddProductCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _productTypeRepository = productTypeRepository;
        _productGroupRepository = productGroupRepository;
        _productUnitRepository = productUnitRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetProductDto> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var productCreated = await Product.InitAsync(request.ProductCode, request.Name, request.TypeId,
            request.GroupId, request.Description,
            request.IsObsolete, request.BuyUnitId,
            request.Buy, request.SellUnitId,
            request.Sell,
            request.Urls,
            _productRepository
            );

        await CheckProductTypeExistAsync(request.TypeId);
        await CheckProductGroupExistAsync(request.GroupId);
        await CheckProductUnitExistAsync(request.BuyUnitId, request.SellUnitId);
        
        _logger.LogInformation("Adding product with name: {Name}", productCreated.Name);
        _productRepository.Add(productCreated);
        
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Product with name: {Name} added successfully", productCreated.Name);
        var productSpecification = new ProductSpecification(productCreated.Id);

        var product = await _productRepository.FindAsync(productSpecification);
        
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