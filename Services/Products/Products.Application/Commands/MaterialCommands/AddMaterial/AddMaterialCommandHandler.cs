using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.EventBus.Interfaces;
using Microsoft.Extensions.Logging;
using Products.Application.Commands.ProductCommands.AddProduct;
using Products.Application.Dtos.Materials;
using Products.Application.IntegrationEvents.Events;
using Products.Domain.MaterialAggregate;
using Products.Domain.MaterialAggregate.DomainServices;
using Products.Domain.MaterialAggregate.Specifications;
using Products.Domain.ProductUnitAggregate;
using Products.Domain.ProductUnitAggregate.Exceptions;

namespace Products.Application.Commands.MaterialCommands.AddMaterial;

public class AddMaterialCommandHandler : ICommandHandler<AddMaterialCommand, GetMaterialDto>
{
    private readonly IRepository<Material> _materialRepository;
    private readonly IMaterialDomainService _materialDomainService;
    private readonly IReadOnlyRepository<ProductUnit> _productUnitRepository;
    
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
        IEventBus eventBus,
        IReadOnlyRepository<ProductUnit> productUnitRepository)
    {
        _materialRepository = repository;
        _materialDomainService = materialDomainService;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _eventBus = eventBus;
        _productUnitRepository = productUnitRepository;
    }

    public async Task<GetMaterialDto> Handle(AddMaterialCommand request, CancellationToken cancellationToken)
    {
        var unitName = string.Empty;
        if (request.SellUnitId.HasValue)
        {
            var sellUnit = await _productUnitRepository.GetByIdAsync(request.SellUnitId.Value);
            if (sellUnit == null)
                throw new ProductUnitNotFoundException(request.SellUnitId.Value);
            unitName = sellUnit.Name;
        }
        
        var material = await _materialDomainService.CreateAsync(request.MaterialCode,
                                    request.Name,
                                    request.TypeId,
                                    request.SellUnitId,
                                    request.SellPrice,
                                    request.Cost,
                                    request.IsObsolete);
        
        _materialRepository.Add(material);

        await _unitOfWork.SaveChangesAsync();
        
        _eventBus.Publish(new MaterialAddedIntegrationEvent(material.Id, material.Name, material.ProductTypeId, 
            material.SellUnitId, unitName, material.SellPrice, material.Cost, material.IsObsolete));

        _logger.LogTrace("Material {materialId} is successfully added", material.Id);

        var createdMaterial = await _materialRepository.FindAsync(new MaterialByIdSpecification(material.Id));
        return _mapper.Map<GetMaterialDto>(createdMaterial);
    }
}