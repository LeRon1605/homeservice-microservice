using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Installations.Application.Dtos;
using Installations.Domain.ContractAggregate;
using Installations.Domain.ContractAggregate.Exceptions;
using Installations.Domain.ContractAggregate.Specifications;
using Installations.Domain.InstallationAggregate;
using Installations.Domain.InstallationAggregate.Exceptions;
using Installations.Domain.InstallerAggregate;
using Installations.Domain.InstallerAggregate.Exceptions;
using Installations.Domain.InstallerAggregate.Specifications;
using Installations.Domain.MaterialAggregate;
using Installations.Domain.MaterialAggregate.Specifications;

namespace Installations.Application.Commands;

public class AddInstallationCommandHandler : ICommandHandler<AddInstallationCommand, InstallationDetailDto>
{
    private readonly IRepository<Installation> _installationRepository;
    private readonly IReadOnlyRepository<Contract> _contractRepository;
    private readonly IReadOnlyRepository<Material> _materialRepository;
    private readonly IReadOnlyRepository<Installer> _installerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddInstallationCommandHandler(IRepository<Installation> installationRepository,
                                         IUnitOfWork unitOfWork,
                                         IReadOnlyRepository<Contract> contractRepository,
                                         IMapper mapper,
                                         IReadOnlyRepository<Material> materialRepository,
                                         IReadOnlyRepository<Installer> installerRepository)
    {
        _installationRepository = installationRepository;
        _unitOfWork = unitOfWork;
        _contractRepository = contractRepository;
        _mapper = mapper;
        _materialRepository = materialRepository;
        _installerRepository = installerRepository;
    }

    public async Task<InstallationDetailDto> Handle(AddInstallationCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.FindAsync(new GetContractByIdWithContractLineSpecification(request.ContractId))
                       ?? throw new ContractNotFoundException(request.ContractId);
        
        var contractLine = contract.ContractLines.FirstOrDefault(x => x.Id == request.ContractLineId)
            ?? throw new ContractLineNotFoundException(request.ContractLineId);
        
        var materials = await _materialRepository.FindListAsync(new GetMaterialsByIdsSpecification(request.InstallationItems.Select(x => x.MaterialId)));
        if (materials.Count != request.InstallationItems.Count)
        {
            throw new InstallationItemNotFoundException(request.InstallationItems.Select(x => x.MaterialId)
                                                                 .Except(materials.Select(x => x.Id)).First());
        } 
        
        // Validate installer
        var installer = await _installerRepository.FindAsync(new InstallerByIdSpecification(request.InstallerId))
            ?? throw new InstallerNotFoundException(request.InstallerId);
        if (installer.IsDeactivated) 
            throw new InstallerIsDeactivatedException(installer.Id);
        
        var installation = new Installation(contractId: request.ContractId,
                contractNo: contract.ContractNo,
                contractLineId: request.ContractLineId,
                productId: contractLine.ProductId,
                productName: contractLine.ProductName,
                productColor: contractLine.Color,
                customerId: contract.CustomerId,
                customerName: contract.CustomerName,
                installationComment: request.InstallationComment,
                installerId: request.InstallerId,
                floorType: request.FloorType,
                installationMetres: request.InstallationMetres,
                installDate: request.InstallDate,
                estimatedStartTime: request.EstimatedStartTime,
                estimatedFinishTime: request.EstimatedFinishTime,
                actualStartTime: request.ActualStartTime,
                actualFinishTime: request.ActualFinishTime,
                fullAddress: contract.InstallationAddress?.FullAddress,
                city: contract.InstallationAddress?.City,
                state: contract.InstallationAddress?.State,
                postalCode: contract.InstallationAddress?.PostalCode);
        
        foreach (var item in request.InstallationItems)
        {
            var material = materials.First(x => x.Id == item.MaterialId);
            installation.AddItem(item.MaterialId, material.Name, item.Quantity, 
                material.SellUnitId, material.SellUnitName, material.Cost, material.SellPrice);
        }
        
        _installationRepository.Add(installation);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<InstallationDetailDto>(installation);
    }
}