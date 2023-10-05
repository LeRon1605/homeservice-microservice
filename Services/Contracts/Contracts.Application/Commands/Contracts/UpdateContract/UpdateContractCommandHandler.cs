using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Exceptions.Resource;
using Contracts.Application.Commands.Contracts.AddContract;
using Contracts.Application.Dtos.Contracts;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Exceptions;
using Contracts.Domain.ContractAggregate.Specifications;
using Contracts.Domain.CustomerAggregate;
using Contracts.Domain.CustomerAggregate.Exceptions;
using Contracts.Domain.ProductAggregate;
using Contracts.Domain.ProductAggregate.Exceptions;
using Contracts.Domain.ProductAggregate.Specifications;
using Contracts.Domain.ProductUnitAggregate;
using Contracts.Domain.ProductUnitAggregate.Exceptions;
using Contracts.Domain.ProductUnitAggregate.Specifications;
using Contracts.Domain.TaxAggregate;
using Contracts.Domain.TaxAggregate.Exceptions;
using Contracts.Domain.TaxAggregate.Specifications;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Contracts.UpdateContract;

public class UpdateContractCommandHandler : ICommandHandler<UpdateContractCommand, ContractDetailDto>
{
    private readonly IRepository<Contract> _contractRepository;
    private readonly IReadOnlyRepository<Product> _productRepository;
    private readonly IReadOnlyRepository<ProductUnit> _productUnitRepository;
    private readonly IReadOnlyRepository<Tax> _taxRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateContractCommandHandler> _logger;
    private readonly IMapper _mapper;
    
    public UpdateContractCommandHandler(
        IRepository<Contract> contractRepository,
        IReadOnlyRepository<Product> productRepository,
        IReadOnlyRepository<ProductUnit> productUnitRepository,
        IRepository<Customer> customerRepository,
        IReadOnlyRepository<Tax> taxRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateContractCommandHandler> logger,
        IMapper mapper)
    {
        _contractRepository = contractRepository;
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;
        _customerRepository = customerRepository;
        _taxRepository = taxRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }
    
    public async Task<ContractDetailDto> Handle(UpdateContractCommand request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.FindAsync(new ContractByIdSpecification(request.Id));
        if (contract == null)
        {
            throw new ContractNotFoundException(request.Id);
        }
        
        contract.UpdateContractInfo(
            request.CustomerNote,
            request.PurchaseOrderNo,
            request.InvoiceNo,
            request.InvoiceDate,
            request.EstimatedInstallationDate,
            request.ActualInstallationDate,
            request.InstallationAddress.FullAddress,
            request.InstallationAddress.City,
            request.InstallationAddress.State,
            request.InstallationAddress.PostalCode,
            request.Status);
        
        await UpdateContractCustomerAsync(contract, request.CustomerId);
        UpdateSalePerson(contract, request.SalePersonId);
        UpdateSupervisor(contract, request.SupervisorId);
        UpdateCustomerServiceRep(contract, request.CustomerServiceRepId);
        await UpdateItemsAsync(contract, request.Items);
        
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Updated contract {ContractId}", contract.Id);

        return _mapper.Map<ContractDetailDto>(contract);
    }

    private async Task UpdateContractCustomerAsync(Contract contract, Guid customerId)
    {
        if (contract.CustomerId != customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                throw new CustomerNotFoundException(customerId);
            }

            contract.UpdateCustomer(customerId);
        }
    }

    private void UpdateSalePerson(Contract contract, Guid salePersonId)
    {
        if (contract.SalePersonId != salePersonId)
        {
            contract.UpdateSalePerson(salePersonId);
        }
    }
    
    private void UpdateSupervisor(Contract contract, Guid? supervisorId)
    {
        if (supervisorId.HasValue && contract.SalePersonId != supervisorId.Value)
        {
            contract.UpdateSalePerson(supervisorId.Value);
        }
    }
    
    private void UpdateCustomerServiceRep(Contract contract, Guid? customerServiceRepId)
    {
        if (customerServiceRepId.HasValue && contract.SalePersonId != customerServiceRepId.Value)
        {
            contract.UpdateSalePerson(customerServiceRepId.Value);
        }
    }

    private async Task UpdateItemsAsync(Contract contract, IList<ContractLineUpdateDto> items)
    {
        var updatedLines = items.Where(x => x.Id.HasValue).ToArray();
        var deletedLines = contract.Items.ExceptBy(updatedLines.Select(x => x.Id), x => x.Id).ToArray();
        var newLines = items.Where(x => !x.Id.HasValue).ToArray();
        
        CheckDuplicateContractLine(updatedLines.Select(x => x.Id!.Value));
        
        var productIds = items.Select(x => x.ProductId).ToArray();
        var products = await _productRepository.FindListAsync(new ProductByIncludedIdsSpecification(productIds));

        var productUnitIds = items.Select(x => x.UnitId).ToArray();
        var productUnits = await _productUnitRepository.FindListAsync(new ProductUnitByIncludedIdsSpecification(productUnitIds));

        var taxIds = items.Where(x => x.TaxId.HasValue).Select(x => x.TaxId!.Value);
        var taxes = await _taxRepository.FindListAsync(new TaxByIncludedIdsSpecification(taxIds));
        
        foreach (var item in deletedLines)
        {
            contract.RemoveItem(item.Id);
        }

        foreach (var item in updatedLines)
        {
            var product = GetProductById(item.ProductId, products);
            var productUnit = GetProductUnitById(item.UnitId, productUnits);
            
            contract.UpdateItem(
                item.Id!.Value, 
                item.ProductId,
                product.Name,
                item.UnitId,
                productUnit.Name,
                item.TaxId,
                GetTaxNameById(item.TaxId, taxes),
                item.Color,
                item.Quantity,
                item.Cost,
                item.SellPrice
            );
        }
        
        foreach (var item in newLines)
        {
            var product = GetProductById(item.ProductId, products);
            var productUnit = GetProductUnitById(item.UnitId, productUnits);
            
            contract.AddContractLine(
                item.ProductId, 
                product.Name, 
                item.UnitId,
                productUnit.Name,
                item.TaxId,
                GetTaxNameById(item.TaxId, taxes),
                item.Color,
                item.Quantity,
                item.Cost,
                item.SellPrice
            );
        }
    }
    
    private void CheckDuplicateContractLine(IEnumerable<Guid> ids)
    {
        var dict = new Dictionary<Guid, bool>();
        foreach (var id in ids)
        {
            if (dict.ContainsKey(id))
            {
                throw new ResourceInvalidOperationException("Duplicate contract line id");
            }
            
            dict.Add(id, true);
        }
    }
    
    private Product GetProductById(Guid productId, IEnumerable<Product> products)
    {
        var product = products.FirstOrDefault(x => x.Id == productId);
        if (product == null)
        {
            throw new ProductNotFoundException(productId);
        }

        return product;
    }
    
    private ProductUnit GetProductUnitById(Guid productUnitId, IEnumerable<ProductUnit> productUnits)
    {
        var productUnit = productUnits.FirstOrDefault(x => x.Id == productUnitId);
        if (productUnit == null)
        {
            throw new ProductUnitNotFoundException(productUnitId);
        }

        return productUnit;
    }
    
    private string? GetTaxNameById(Guid? taxId, IEnumerable<Tax> taxes)
    {
        if (taxId.HasValue)
        {
            var tax = taxes.FirstOrDefault(x => x.Id == taxId);
            if (tax == null)
            {
                throw new TaxNotFoundException(taxId.Value);
            }

            return tax.Name;
        }

        return null;
    }
}