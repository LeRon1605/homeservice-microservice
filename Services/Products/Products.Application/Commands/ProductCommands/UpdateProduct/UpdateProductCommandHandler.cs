using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Products.Application.Commands.ProductCommands.Validator;
using Products.Application.Dtos;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductAggregate.Exceptions;
using Products.Domain.ProductAggregate.Specifications;

namespace Products.Application.Commands.ProductCommands.UpdateProduct;

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, GetProductDto>
{
    private readonly IRepository<Product> _productRepository;

    private readonly IProductValidator _productValidator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateProductCommandHandler(IRepository<Product> productRepository, 
                                       IUnitOfWork unitOfWork, 
                                       IMapper mapper, 
                                       IProductValidator productValidator)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _productValidator = productValidator;
    }
    
    public async Task<GetProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindAsync(new ProductByIdSpecification(request.Id));
        //var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }

        await _productValidator.CheckProductTypeExistAsync(request.TypeId);
        await _productValidator.CheckProductGroupExistAsync(request.GroupId);
        await _productValidator.CheckProductUnitExistAsync(request.BuyUnitId, request.SellUnitId);
        
        await product.UpdateAsync(
            request.ProductCode, request.Name, request.TypeId,
                request.GroupId, request.Description,
                request.IsObsolete, request.BuyUnitId,
                request.Buy, request.SellUnitId,
                request.Sell,
                request.Urls,
                _productRepository
            );
        
        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();
        
        return _mapper.Map<GetProductDto>(product);
    }
}