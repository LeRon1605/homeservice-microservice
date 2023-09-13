using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Products.Application.Dtos;
using Products.Domain.ProductAggregate;

namespace Products.Application.Commands.ProductCommands.AddProduct;

public class AddProductCommandHandler : ICommandHandler<AddProductCommand, GetProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AddProductCommandHandler> _logger;

    public AddProductCommandHandler(IUnitOfWork unitOfWork,
                                    IRepository<Product> productRepository,
                                    IMapper mapper,
                                    ILogger<AddProductCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GetProductDto> Handle(AddProductCommand request,
                                      CancellationToken cancellationToken)
    {
        var product = new Product(request.Name);

        _logger.LogInformation("Adding product with name: {Name}", product.Name);
        
        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Product with name: {Name} added successfully", product.Name);
        
        return _mapper.Map<GetProductDto>(product); 
    }
}