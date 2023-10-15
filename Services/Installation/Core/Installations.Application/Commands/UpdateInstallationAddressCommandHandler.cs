using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Installations.Domain.InstallationAggregate;
using Installations.Domain.InstallationAggregate.Specifications;

namespace Installations.Application.Commands;

public class UpdateInstallationAddressCommandHandler : ICommandHandler<UpdateInstallationAddressCommand>
{
    private readonly IRepository<Installation> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateInstallationAddressCommandHandler(IRepository<Installation> repository,
                                                   IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateInstallationAddressCommand request,
                             CancellationToken cancellationToken)
    {
        var installationToUpdate =
            await _repository.FindListAsync(new InstallationByIdSpecification(request.ContractId));
        
        foreach (var installation in installationToUpdate)
            installation.UpdateAddress(request.InstallationAddress?.FullAddress, request.InstallationAddress?.City,
                request.InstallationAddress?.State, request.InstallationAddress?.PostalCode);
        
        await _unitOfWork.SaveChangesAsync();
    }
}