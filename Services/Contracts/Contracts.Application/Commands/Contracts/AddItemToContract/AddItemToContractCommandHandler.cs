using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Application.Dtos.Contracts;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Exceptions;
using Contracts.Domain.ContractAggregate.Specifications;
using Contracts.Domain.ProductAggregate;
using Contracts.Domain.ProductAggregate.Exceptions;
using Contracts.Domain.ProductUnitAggregate;
using Contracts.Domain.ProductUnitAggregate.Exceptions;
using Contracts.Domain.TaxAggregate;
using Contracts.Domain.TaxAggregate.Exceptions;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Contracts.AddItemToContract;

public class AddItemToContractCommandHandler : ICommandHandler<AddItemToContractCommand, ContractLineDto>
{
    private readonly IRepository<Contract> _contractRepository;
    private readonly IReadOnlyRepository<Product> _productRepository;
    private readonly IReadOnlyRepository<ProductUnit> _productUnitRepository;
    private readonly IReadOnlyRepository<Tax> _taxRepository;
    private readonly ILogger<AddItemToContractCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public AddItemToContractCommandHandler(
        IRepository<Contract> contractRepository,
        IReadOnlyRepository<Product> productRepository,
        IReadOnlyRepository<ProductUnit> productUnitRepository,
        IReadOnlyRepository<Tax> taxRepository,
        ILogger<AddItemToContractCommandHandler> logger,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _taxRepository = taxRepository;
        _productUnitRepository = productUnitRepository;
        _productRepository = productRepository;
        _contractRepository = contractRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<ContractLineDto> Handle(AddItemToContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.FindAsync(new ContractByIdSpecification(request.ContractId));
        if (contract == null)
        {
            throw new ContractNotFoundException(request.ContractId);
        }
        
        var product = await _productRepository.GetByIdAsync(request.ProductId);
        if (product == null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }
        
        var productUnit = await _productUnitRepository.GetByIdAsync(request.UnitId);
        if (productUnit == null)
        {
            throw new ProductUnitNotFoundException(request.UnitId);
        }
        
        ContractLine contractLine;
        if (request.TaxId.HasValue)
        {
            var tax = await _taxRepository.GetByIdAsync(request.TaxId.Value);
            if (tax == null)
            {
                throw new TaxNotFoundException(request.TaxId.Value);    
            }

            contractLine = contract.AddContractLine(
                request.ProductId, 
                product.Name, 
                request.UnitId,
                productUnit.Name,
                request.TaxId,
                tax.Name,
                tax.Value,
                request.Color,
                request.Quantity,
                request.Cost,
                request.SellPrice
            );
        }
        else
        {
            contractLine = contract.AddContractLine(
                request.ProductId, 
                product.Name, 
                request.UnitId,
                productUnit.Name,
                null,
                null,
                null,
                request.Color,
                request.Quantity,
                request.Cost,
                request.SellPrice
            );
        }
        
        _contractRepository.Update(contract);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Added new item to contract: {ContractId}", request.ContractId);
        return _mapper.Map<ContractLineDto>(contractLine);
    }
}