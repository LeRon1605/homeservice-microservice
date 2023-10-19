using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.Domain.Data;
using Installations.Application.IntegrationEvents.Events.Materials;
using Installations.Domain.MaterialAggregate;
using Microsoft.Extensions.Logging;

namespace Installations.Application.IntegrationEvents.Handlers.Materials;

public class MaterialDeletedIntegrationEventHandler : IIntegrationEventHandler<MaterialDeletedIntegrationEvent>
{
    private readonly IRepository<Material> _materialRepository;
    private readonly ILogger<MaterialDeletedIntegrationEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public MaterialDeletedIntegrationEventHandler(IRepository<Material> materialRepository,
                                                  ILogger<MaterialDeletedIntegrationEventHandler> logger,
                                                  IUnitOfWork unitOfWork)
    {
        _materialRepository = materialRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(MaterialDeletedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event add material: {IntegrationEventId} -  ({@IntegrationEvent})",
            @event.Id, @event);

        var material = await _materialRepository.GetByIdAsync(@event.MaterialId);
        _materialRepository.Delete(material!);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Integration event add material handled: {IntegrationEventId} -  ({@IntegrationEvent})",
            @event.Id, @event);
    }
}