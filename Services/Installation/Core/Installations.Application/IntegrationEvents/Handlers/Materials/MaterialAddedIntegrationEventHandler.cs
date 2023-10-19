using BuildingBlocks.Application.IntegrationEvent;
using BuildingBlocks.Domain.Data;
using Installations.Application.IntegrationEvents.Events.Materials;
using Installations.Domain.MaterialAggregate;
using Microsoft.Extensions.Logging;

namespace Installations.Application.IntegrationEvents.Handlers.Materials;

public class MaterialAddedIntegrationEventHandler : IIntegrationEventHandler<MaterialAddedIntegrationEvent>
{
    private readonly IRepository<Material> _materialRepository;
    private readonly ILogger<MaterialAddedIntegrationEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public MaterialAddedIntegrationEventHandler(IRepository<Material> materialRepository,
                                                ILogger<MaterialAddedIntegrationEventHandler> logger,
                                                IUnitOfWork unitOfWork)
    {
        _materialRepository = materialRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(MaterialAddedIntegrationEvent @event)
    {
        _logger.LogInformation("Handling integration event add material: {IntegrationEventId} -  ({@IntegrationEvent})", @event.Id, @event);
        var material = new Material(@event.MaterialId,
            @event.Name,
            @event.ProductTypeId,
            @event.SellUnitId,
            @event.SellUnitName,
            @event.SellPrice,
            @event.Cost,
            @event.IsObsolete);
        _materialRepository.Add(material);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Integration event add material handled: {IntegrationEventId} -  ({@IntegrationEvent})", @event.Id, @event);
    }
}