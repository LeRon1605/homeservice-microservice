using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.Domain.Data;
using Installations.Application.IntegrationEvents.Events.Employees;
using Installations.Domain.InstallerAggregate;

namespace Installations.Application.IntegrationEvents.Handlers.Employees;

public class EmployeeDeactivatedIntegrationEventHandler : IIntegrationEventHandler<EmployeeDeactivatedIntegrationEvent>
{
    private readonly IRepository<Installer> _installerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeDeactivatedIntegrationEventHandler(IRepository<Installer> installerRepository,
                                                      IUnitOfWork unitOfWork)
    {
        _installerRepository = installerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(EmployeeDeactivatedIntegrationEvent @event)
    {
        var installer = await _installerRepository.GetByIdAsync(@event.EmployeeId);
        if (installer == null) return;
        
        installer.Deactivate();
        await _unitOfWork.SaveChangesAsync();
    }
}