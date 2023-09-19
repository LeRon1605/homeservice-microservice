using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Products.Application.Commands.ProductCommands.AddProduct;
using Products.Application.Dtos;
using Products.Domain.MaterialAggregate;
using Products.Domain.MaterialAggregate.Exceptions;
using Products.Domain.MaterialAggregate.Specifications;
using Products.Domain.ProductAggregate.Exceptions;
using Products.Domain.ProductTypeAggregate;

namespace Products.Application.Commands.MaterialCommands.AddMaterial;

public class AddMaterialCommandHandler : ICommandHandler<AddMaterialCommand, GetMaterialDto>
{
    private readonly IRepository<Material> _materialRepository;
    private readonly IRepository<ProductType> _productTypeRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddProductCommandHandler> _logger;
    private readonly IMapper _mapper;

    public AddMaterialCommandHandler(IRepository<Material> repository,
                                     IUnitOfWork unitOfWork,
                                     ILogger<AddProductCommandHandler> logger,
                                     IMapper mapper,
                                     IRepository<ProductType> productTypeRepository)
    {
        _materialRepository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _productTypeRepository = productTypeRepository;
    }

    public async Task<GetMaterialDto> Handle(AddMaterialCommand request, CancellationToken cancellationToken)
    {
        var isProductTypeExisted = await _productTypeRepository.AnyAsync(request.ProductTypeId); 
        var isMaterialCodeDuplicated = await _materialRepository.AnyAsync(
                                           new MaterialCodeDuplicatedSpecification(request.MaterialCode));
        
        if (!isProductTypeExisted)
            throw new ProductTypeNotFoundException(request.ProductTypeId);
        
        if (isMaterialCodeDuplicated)
            throw new MaterialCodeExistedException(request.MaterialCode);
        
        var material = new Material(request.MaterialCode,
                                    request.Name,
                                    request.ProductTypeId,
                                    request.SellUnitId,
                                    request.SellPrice,
                                    request.Cost,
                                    request.IsObsolete);
        
        _materialRepository.Add(material);

        await _unitOfWork.SaveChangesAsync();

        _logger.LogTrace("Material {materialId} is successfully added", material.Id);

        return _mapper.Map<GetMaterialDto>(material);
    }
}