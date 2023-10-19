using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Installations.Application.Dtos;
using Installations.Domain.ContractAggregate;
using Installations.Domain.ContractAggregate.Exceptions;
using Installations.Domain.ContractAggregate.Specifications;
using Installations.Domain.InstallationAggregate;
using Installations.Domain.InstallationAggregate.Exceptions;
using Installations.Domain.InstallationAggregate.Specifications;
using Installations.Domain.MaterialAggregate;
using Installations.Domain.MaterialAggregate.Specifications;

namespace Installations.Application.Commands;

public class UpdateInstallationCommandHandler : ICommandHandler<UpdateInstallationCommand, InstallationDto>
{
    private readonly IRepository<Installation> _installationRepository;
    private readonly IRepository<Material> _materialRepository;
    private readonly IRepository<Contract> _contractRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateInstallationCommandHandler(IRepository<Installation> installationRepository,
                                            IUnitOfWork unitOfWork,
                                            IRepository<Contract> contractRepository,
                                            IRepository<Material> materialRepository,
                                            IMapper mapper)
    {
        _installationRepository = installationRepository;
        _unitOfWork = unitOfWork;
        _contractRepository = contractRepository;
        _materialRepository = materialRepository;
        _mapper = mapper;
    }

    public async Task<InstallationDto> Handle(UpdateInstallationCommand request, CancellationToken cancellationToken)
    {
        var installation = await _installationRepository.FindAsync(new InstallationByIdSpecification(request.InstallationId));
        if (installation is null)
            throw new InstallationNotFoundException(request.InstallationId);

        var contract = await _contractRepository.FindAsync(new GetContractWithContractLineSpecification(installation.ContractId))
                       ?? throw new ContractNotFoundException(installation.ContractId);
        
        var contractLine = contract.ContractLines.FirstOrDefault(x => x.Id == request.ContractLineId)
            ?? throw new ContractLineNotFoundException(request.ContractLineId);
        
        var materials = await _materialRepository.FindListAsync(new GetMaterialsByIdsSpecification(request.InstallationItems.Select(x => x.MaterialId)));
        if (materials.Count != request.InstallationItems.Count)
        {
            throw new InstallationItemNotFoundException(materials.Select(x => x.Id)
                                                                 .Except(request.InstallationItems
                                                                    .Select(x => x.MaterialId)).First());
        } 
        
        // Update the installation
        installation.Update(contractLineId: request.ContractLineId,
                productId: contractLine.ProductId,
                productName: contractLine.ProductName,
                productColor: contractLine.Color,
                installerId: request.InstallerId,
                floorType: request.FloorType,
                installationMetres: request.InstallationMetres,
                installationComment: request.InstallationComment,
                installDate: request.InstallDate,
                estimatedStartTime: request.EstimatedStartTime,
                estimatedFinishTime: request.EstimatedFinishTime,
                actualStartTime: request.ActualStartTime,
                actualFinishTime: request.ActualFinishTime);
        
        installation.RemoveItems();
        foreach (var item in request.InstallationItems)
        {
            var material = materials.First(x => x.Id == item.MaterialId);
            installation.AddItem(item.MaterialId, material.Name, item.Quantity, 
                material.SellUnitId, material.SellUnitName, material.Cost, material.SellPrice);
        }
        
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<InstallationDto>(installation);
    }
}