using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.ProductAggregate;
using Contracts.Domain.ProductAggregate.Exceptions;

namespace Contracts.Application.Commands.Products.UpdateProduct;

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHandler(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        
        product.Update(request.Name, request.ProductUnitId, request.SellPrice, request.Colors?.Split(",").ToList());
        _productRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();
    }
}