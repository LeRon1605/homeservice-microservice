using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.Domain.Data;
using Installations.Application.IntegrationEvents.Events.Materials;
using Installations.Domain.MaterialAggregate;
using Microsoft.Extensions.Logging;

namespace Installations.Application.IntegrationEvents.Handlers.Materials;

public class MaterialUpdatedIntegrationEventHandler : IIntegrationEventHandler<MaterialUpdatedIntegrationEvent>
{
    
    private readonly IRepository<Material> _materialRepository;
    private readonly ILogger<MaterialAddedIntegrationEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public MaterialUpdatedIntegrationEventHandler(IRepository<Material> materialRepository,
                                                ILogger<MaterialAddedIntegrationEventHandler> logger,
                                                IUnitOfWork unitOfWork)
    {
        _materialRepository = materialRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(MaterialUpdatedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event add material: {IntegrationEventId} -  ({@IntegrationEvent})",
            @event.Id, @event);

        var material = await _materialRepository.GetByIdAsync(@event.MaterialId);
        material!.Update(@event.Name, @event.ProductTypeId, @event.SellUnitId, @event.SellUnitName, @event.SellPrice, @event.Cost, @event.IsObsolete);
        _materialRepository.Update(material);
        await _unitOfWork.SaveChangesAsync();

        _logger.LogInformation("Integration event add material handled: {IntegrationEventId} -  ({@IntegrationEvent})",
            @event.Id, @event);
    }
}