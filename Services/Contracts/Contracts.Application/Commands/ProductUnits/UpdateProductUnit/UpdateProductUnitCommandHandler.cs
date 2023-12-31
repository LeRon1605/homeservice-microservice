using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.ProductUnitAggregate;
using Contracts.Domain.ProductUnitAggregate.Exceptions;

namespace Contracts.Application.Commands.ProductUnits.UpdateProductUnit;

public class UpdateProductUnitCommandHandler : ICommandHandler<UpdateProductUnitCommand>
{
    private readonly IRepository<ProductUnit> _productUnitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductUnitCommandHandler(IRepository<ProductUnit> productUnitRepository, IUnitOfWork unitOfWork)
    {
        _productUnitRepository = productUnitRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(UpdateProductUnitCommand request, CancellationToken cancellationToken)
    {
        var productUnit = await _productUnitRepository.GetByIdAsync(request.Id);
        if (productUnit == null)
        {
            throw new ProductUnitNotFoundException(request.Id);
        }
        
        productUnit.Update(request.Name);
        
        _productUnitRepository.Update(productUnit);
        await _unitOfWork.SaveChangesAsync();
    }
}