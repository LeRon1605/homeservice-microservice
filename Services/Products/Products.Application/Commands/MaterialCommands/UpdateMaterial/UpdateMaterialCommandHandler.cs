using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.EventBus.Interfaces;
using Microsoft.Extensions.Logging;
using Products.Application.Dtos.Materials;
using Products.Application.IntegrationEvents.Events;
using Products.Domain.MaterialAggregate;
using Products.Domain.MaterialAggregate.DomainServices;
using Products.Domain.MaterialAggregate.Exceptions;
using Products.Domain.MaterialAggregate.Specifications;

namespace Products.Application.Commands.MaterialCommands.UpdateMaterial;

public class UpdateMaterialCommandHandler : ICommandHandler<UpdateMaterialCommand, GetMaterialDto>
{
    private readonly IRepository<Material> _materialRepository;
    private readonly IMaterialDomainService _materialDomainService;
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateMaterialCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public UpdateMaterialCommandHandler(
        IRepository<Material> repository,
        IMaterialDomainService materialDomainService,
        IUnitOfWork unitOfWork,
        ILogger<UpdateMaterialCommandHandler> logger,
        IMapper mapper,
        IEventBus eventBus)
    {
        _materialRepository = repository;
        _materialDomainService = materialDomainService;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _eventBus = eventBus;
    }
    
    public async Task<GetMaterialDto> Handle(UpdateMaterialCommand request, CancellationToken cancellationToken)
    {
        var material = await _materialRepository.GetByIdAsync(request.Id);
        if (material == null)
        {
            throw new MaterialNotFoundException(request.Id);
        }
        
        _logger.LogInformation("Updating material with id: {materialId}", request.Id);

        await _materialDomainService.UpdateAsync(material,
            request.MaterialCode,
            request.Name,
            request.TypeId,
            request.SellUnitId,
            request.SellPrice,
            request.Cost,
            request.IsObsolete);

        _materialRepository.Update(material);
        await _unitOfWork.SaveChangesAsync();
        
        _eventBus.Publish(new MaterialUpdatedIntegrationEvent(material.Id, material.Name, material.IsObsolete));
        
        _logger.LogInformation("Updated material with id: {materialId}", request.Id);

        var updatedMaterial = await _materialRepository.FindAsync(new MaterialByIdSpecification(material.Id));
        return _mapper.Map<GetMaterialDto>(updatedMaterial);
    }
}