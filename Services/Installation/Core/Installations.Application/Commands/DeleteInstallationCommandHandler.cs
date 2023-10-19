using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Installations.Domain.InstallationAggregate;
using Installations.Domain.InstallationAggregate.Exceptions;

namespace Installations.Application.Commands;

public class DeleteInstallationCommandHandler : ICommandHandler<DeleteInstallationCommand>
{
    private readonly IRepository<Installation> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInstallationCommandHandler(IRepository<Installation> repository,
                                            IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteInstallationCommand request, CancellationToken cancellationToken)
    {
        var installation = await _repository.GetByIdAsync(request.InstallationId);
        
        if (installation is null)
            throw new InstallationNotFoundException(request.InstallationId);

        installation.Delete();
        _repository.Delete(installation);
        await _unitOfWork.SaveChangesAsync();
    }
}