using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.Domain.Data;
using Installations.Application.IntegrationEvents.Events.Employees;
using Installations.Domain.InstallerAggregate;
using Microsoft.Extensions.Logging;

namespace Installations.Application.IntegrationEvents.Handlers.Employees;

public class EmployeeAddedIntegrationEventHandler : IIntegrationEventHandler<EmployeeAddedIntegrationEvent>
{
    private readonly IRepository<Installer> _installerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EmployeeAddedIntegrationEventHandler> _logger;

    public EmployeeAddedIntegrationEventHandler(IRepository<Installer> installerRepository,
                                                IUnitOfWork unitOfWork,
                                                ILogger<EmployeeAddedIntegrationEventHandler> logger)
    {
        _installerRepository = installerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(EmployeeAddedIntegrationEvent @event)
    {
        _logger.LogInformation($"Handling integration event: {@event.Id} at {nameof(EmployeeAddedIntegrationEventHandler)}");
        if (@event.RoleName != "Installer") return;
        
        var installer = new Installer(@event.EmployeeId, @event.FullName, @event.Email, @event.Phone);
        
        _installerRepository.Add(installer);
        
        await _unitOfWork.SaveChangesAsync();
    }
}