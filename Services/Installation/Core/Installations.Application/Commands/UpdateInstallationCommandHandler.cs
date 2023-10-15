using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Installations.Domain.InstallationAggregate;
using Installations.Domain.InstallationAggregate.Exceptions;
using Installations.Domain.InstallationAggregate.Specifications;

namespace Installations.Application.Commands;

public class UpdateInstallationCommandHandler : ICommandHandler<UpdateInstallationCommand>
{
    private readonly IRepository<Installation> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateInstallationCommandHandler(IRepository<Installation> repository,
                                            IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateInstallationCommand request, CancellationToken cancellationToken)
    {
        var installation = await _repository.FindAsync(new InstallationByIdSpecification(request.InstallationId));
        if (installation is null)
        {
            throw new InstallationNotFoundException(request.InstallationId);
        }
        
        installation.Update(contractLineId: request.ContractLineId,
                productId: request.ProductId,
                productName: request.ProductName,
                productColor: request.ProductColor,
                installerId: request.InstallerId,
                floorType: request.FloorType,
                installationMetres: request.InstallationMetres,
                installationComment: request.InstallationComment,
                installDate: request.InstallDate,
                estimatedStartTime: request.EstimatedStartTime,
                estimatedFinishTime: request.EstimatedFinishTime,
                actualStartTime: request.ActualStartTime,
                actualFinishTime: request.ActualFinishTime,
                fullAddress: request.InstallationAddress?.FullAddress,
                city: request.InstallationAddress?.City,
                state: request.InstallationAddress?.State,
                postalCode: request.InstallationAddress?.PostalCode);
        
        installation.RemoveItems();
        foreach (var item in request.InstallationItems)
        {
            installation.AddItem(item.MaterialId, item.MaterialName, item.Quantity, item.UnitId, item.UnitName, item.Cost, item.SellPrice);
        }
        
        await _unitOfWork.SaveChangesAsync();
    }
}