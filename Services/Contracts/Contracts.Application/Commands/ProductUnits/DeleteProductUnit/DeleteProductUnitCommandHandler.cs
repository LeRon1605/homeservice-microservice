using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.ProductUnitAggregate;
using Contracts.Domain.ProductUnitAggregate.Exceptions;

namespace Contracts.Application.Commands.ProductUnits.DeleteProductUnit;

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
            throw new ProductUnitNotFoundException(request.Id);
        }
        
        _productUnitRepository.Delete(productUnit);
        await _unitOfWork.SaveChangesAsync();
    }
}