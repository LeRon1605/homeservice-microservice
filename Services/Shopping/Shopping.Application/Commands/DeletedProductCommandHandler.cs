using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Shopping.Domain.ProductAggregate;
using Shopping.Domain.ProductAggregate.Exceptions;
using Shopping.Domain.ProductUnitAggregate.Exceptions;

namespace Shopping.Application.Commands;

public class DeletedProductCommandHandler : ICommandHandler<DeletedProductCommand>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletedProductCommandHandler(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeletedProductCommand request, CancellationToken cancellationToken)
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