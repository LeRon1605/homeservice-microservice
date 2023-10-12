using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Installations.Domain.InstallationAggregate;
using Installations.Domain.InstallationAggregate.Enums;

namespace Installations.Application.Commands;

public class AddInstallationCommandHandler : ICommandHandler<AddInstallationCommand>
{
    private readonly IRepository<Installation> _installationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddInstallationCommandHandler(IRepository<Installation> installationRepository,
                                         IUnitOfWork unitOfWork)
    {
        _installationRepository = installationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddInstallationCommand request, CancellationToken cancellationToken)
    {
        var installation = new Installation(contractId: request.ContractId,
                contractNo: request.ContractNo,
                contractLineId: request.ContractLineId,
                productId: request.ProductId,
                productName: request.ProductName,
                productColor: request.ProductColor,
                customerId: request.CustomerId,
                customerName: request.CustomerName,
                installationComment: request.InstallationComment,
                installerId: request.InstallerId,
                floorType: request.FloorType,
                installationMetres: request.InstallationMetres,
                installDate: request.InstallDate,
                estimatedStartTime: request.EstimatedStartTime,
                estimatedFinishTime: request.EstimatedFinishTime,
                actualStartTime: request.ActualStartTime,
                actualFinishTime: request.ActualFinishTime,
                fullAddress: request.InstallationAddress?.FullAddress,
                city: request.InstallationAddress?.City,
                state: request.InstallationAddress?.State,
                postalCode: request.InstallationAddress?.PostalCode);
        
        foreach (var item in request.InstallationItems)
        {
            installation.AddItem(item.MaterialId, item.MaterialName, item.Quantity, 
                item.UnitId, item.UnitName, item.Cost, item.SellPrice);
        }
        
        _installationRepository.Add(installation);
        await _unitOfWork.SaveChangesAsync();
    }
}