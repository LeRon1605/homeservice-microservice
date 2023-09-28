using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Shopping.Domain.ProductAggregate.Exceptions;
using Shopping.Domain.ProductUnitAggregate;

namespace Shopping.Application.Commands;

public class DeleteProductUnitCommandHandler : ICommandHandler<DeleteProductUnitCommand>
{
    private readonly IRepository<ProductUnit> _productUnitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductUnitCommandHandler(IRepository<ProductUnit> productUnitRepository, IUnitOfWork unitOfWork)
    {
        _productUnitRepository = productUnitRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteProductUnitCommand request, CancellationToken cancellationToken)
    {
        var productUnit = await _productUnitRepository.GetByIdAsync(request.Id);
        if (productUnit == null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        _productUnitRepository.Delete(productUnit);
        await _unitOfWork.SaveChangesAsync();
    }
}