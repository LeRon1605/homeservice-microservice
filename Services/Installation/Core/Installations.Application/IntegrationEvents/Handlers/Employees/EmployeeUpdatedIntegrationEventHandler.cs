using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.Domain.Data;
using Installations.Application.IntegrationEvents.Events.Employees;
using Installations.Domain.InstallerAggregate;

namespace Installations.Application.IntegrationEvents.Handlers.Employees;

public class EmployeeUpdatedIntegrationEventHandler : IIntegrationEventHandler<EmployeeUpdatedIntegrationEvent>
{
    private readonly IRepository<Installer> _installerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeUpdatedIntegrationEventHandler(IRepository<Installer> installerRepository,
                                                  IUnitOfWork unitOfWork)
    {
        _installerRepository = installerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(EmployeeUpdatedIntegrationEvent @event)
    {
        if (@event.RoleName != "Installer") return;
        
        var installer = await _installerRepository.GetByIdAsync(@event.EmployeeId);

        installer!.Update(@event.FullName, @event.Email, @event.Phone);

        await _unitOfWork.SaveChangesAsync();
    }
}