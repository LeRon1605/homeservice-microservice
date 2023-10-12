using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.EventBus.Interfaces;
using Contracts.Application.IntegrationEvents.Events.Materials;
using Microsoft.Extensions.Logging;
using Products.Application.Commands.ProductCommands.AddProduct;
using Products.Application.Dtos;
using Products.Domain.MaterialAggregate;
using Products.Domain.MaterialAggregate.DomainServices;
using Products.Domain.MaterialAggregate.Specifications;

namespace Products.Application.Commands.MaterialCommands.AddMaterial;

public class AddMaterialCommandHandler : ICommandHandler<AddMaterialCommand, GetMaterialDto>
{
    private readonly IRepository<Material> _materialRepository;
    private readonly IMaterialDomainService _materialDomainService;
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddProductCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public AddMaterialCommandHandler(
        IRepository<Material> repository,
        IMaterialDomainService materialDomainService,
        IUnitOfWork unitOfWork,
        ILogger<AddProductCommandHandler> logger,
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

    public async Task<GetMaterialDto> Handle(AddMaterialCommand request, CancellationToken cancellationToken)
    {
        var material = await _materialDomainService.CreateAsync(request.MaterialCode,
                                    request.Name,
                                    request.TypeId,
                                    request.SellUnitId,
                                    request.SellPrice,
                                    request.Cost,
                                    request.IsObsolete);
        
        _materialRepository.Add(material);

        await _unitOfWork.SaveChangesAsync();
        
        _eventBus.Publish(new MaterialAddedIntegrationEvent(material.Id, material.Name, material.IsObsolete));

        _logger.LogTrace("Material {materialId} is successfully added", material.Id);

        var createdMaterial = await _materialRepository.FindAsync(new MaterialByIdSpecification(material.Id));
        return _mapper.Map<GetMaterialDto>(createdMaterial);
    }
}