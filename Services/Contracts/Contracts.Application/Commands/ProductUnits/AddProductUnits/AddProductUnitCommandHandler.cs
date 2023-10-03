using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.ProductUnitAggregate;

namespace Contracts.Application.Commands.ProductUnits.AddProductUnits;

public class AddProductUnitCommandHandler : ICommandHandler<AddProductUnitCommand>
{
    private readonly IRepository<ProductUnit> _productUnitRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddProductUnitCommandHandler(IRepository<ProductUnit> productUnitRepository, IUnitOfWork unitOfWork)
    {
        _productUnitRepository = productUnitRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddProductUnitCommand request, CancellationToken cancellationToken)
    {
        var productUnitCreated = new ProductUnit(request.Id, request.Name);
        
        _productUnitRepository.Add(productUnitCreated);
        await _unitOfWork.SaveChangesAsync();
    }
}