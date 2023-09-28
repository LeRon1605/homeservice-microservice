using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Shopping.Domain.ProductAggregate;
using Shopping.Domain.ProductAggregate.Exceptions;

namespace Shopping.Application.Commands;

public class UpdatedProductCommandHandler : ICommandHandler<UpdatedProductCommand>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatedProductCommandHandler(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(UpdatedProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        
        product.Update(request.Name,request.ProductGroupId, request.SellPrice, request.ProductUnitId);
        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();
    }
}