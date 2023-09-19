using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Products.Application.Commands.ProductCommands.Validator;
using Products.Application.Dtos;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductAggregate.Specifications;

namespace Products.Application.Commands.ProductCommands.AddProduct;

public class AddProductCommandHandler : ICommandHandler<AddProductCommand, GetProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Product> _productRepository;
    private readonly IProductValidator _productValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<AddProductCommandHandler> _logger;
   

    public AddProductCommandHandler(IUnitOfWork unitOfWork,
        IRepository<Product> productRepository,
        IMapper mapper,
        ILogger<AddProductCommandHandler> logger,
        IProductValidator productValidator)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
        _productValidator = productValidator;
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

        await _productValidator.CheckProductTypeExistAsync(request.TypeId);
        await _productValidator.CheckProductGroupExistAsync(request.GroupId);
        await _productValidator.CheckProductUnitExistAsync(request.BuyUnitId, request.SellUnitId);
        
        _logger.LogInformation("Adding product with name: {Name}", productCreated.Name);
        _productRepository.Add(productCreated);
        
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Product with name: {Name} added successfully", productCreated.Name);
        var productSpecification = new ProductByIdSpecification(productCreated.Id);

        var product = await _productRepository.FindAsync(productSpecification);
        
        return _mapper.Map<GetProductDto>(product);
    }
}