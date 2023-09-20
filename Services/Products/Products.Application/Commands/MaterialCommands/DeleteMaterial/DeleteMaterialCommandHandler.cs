using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Products.Domain.MaterialAggregate;
using Products.Domain.MaterialAggregate.Exceptions;

namespace Products.Application.Commands.MaterialCommands.DeleteMaterial;

public class DeleteMaterialCommandHandler : ICommandHandler<DeleteMaterialCommand>
{
    private readonly IRepository<Material> _materialRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteMaterialCommandHandler(
        IRepository<Material> materialRepository,
        IUnitOfWork unitOfWork)
    {
        _materialRepository = materialRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteMaterialCommand request, CancellationToken cancellationToken)
    {
        var material = await _materialRepository.GetByIdAsync(request.Id);
        if (material == null)
        {
            throw new MaterialNotFoundException(request.Id);
        }
        
        _materialRepository.Delete(material);
        await _unitOfWork.SaveChangesAsync();
    }
}