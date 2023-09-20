using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Products.Application.Dtos;
using Products.Domain.MaterialAggregate;
using Products.Domain.MaterialAggregate.DomainServices;
using Products.Domain.MaterialAggregate.Exceptions;

namespace Products.Application.Commands.MaterialCommands.EditMaterial;

public class EditMaterialCommandHandler : ICommandHandler<EditMaterialCommand, GetMaterialDto>
{
    private readonly IRepository<Material> _materialRepository;
    private readonly IMaterialDomainService _materialDomainService;
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EditMaterialCommandHandler> _logger;
    private readonly IMapper _mapper;

    public EditMaterialCommandHandler(
        IRepository<Material> repository,
        IMaterialDomainService materialDomainService,
        IUnitOfWork unitOfWork,
        ILogger<EditMaterialCommandHandler> logger,
        IMapper mapper)
    {
        _materialRepository = repository;
        _materialDomainService = materialDomainService;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<GetMaterialDto> Handle(EditMaterialCommand request, CancellationToken cancellationToken)
    {
        var material = await _materialRepository.GetByIdAsync(request.Id);
        if (material == null)
        {
            throw new MaterialNotFoundException(request.Id);
        }

        await _materialDomainService.UpdateAsync(material,
            request.MaterialCode,
            request.Name,
            request.ProductTypeId,
            request.SellUnitId,
            request.SellPrice,
            request.Cost,
            request.IsObsolete);

        _materialRepository.Update(material);
        await _unitOfWork.SaveChangesAsync();
        
        return _mapper.Map<GetMaterialDto>(material);
    }
}