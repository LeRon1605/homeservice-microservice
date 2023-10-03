using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.ProductAggregate;
using Contracts.Domain.ProductUnitAggregate.Exceptions;
using Shopping.Application.Commands.Products.DeleteProduct;

namespace Contracts.Application.Commands.Products.DeleteProduct;

public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new ProductUnitNotFoundException(request.Id);
        }
        
        _productRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync();
    }
}