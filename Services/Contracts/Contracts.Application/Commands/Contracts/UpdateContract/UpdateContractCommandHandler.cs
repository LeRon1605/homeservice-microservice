using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Exceptions.Resource;
using BuildingBlocks.EventBus.Interfaces;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Dtos.Contracts.ContractUpdate;
using Contracts.Application.IntegrationEvents.Events.Contracts;
using Contracts.Application.IntegrationEvents.Events.Contracts.EventDtos;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Exceptions;
using Contracts.Domain.ContractAggregate.Specifications;
using Contracts.Domain.CustomerAggregate;
using Contracts.Domain.CustomerAggregate.Exceptions;
using Contracts.Domain.MaterialAggregate;
using Contracts.Domain.MaterialAggregate.Exceptions;
using Contracts.Domain.MaterialAggregate.Specifications;
using Contracts.Domain.PaymentMethodAggregate;
using Contracts.Domain.PaymentMethodAggregate.Exceptions;
using Contracts.Domain.PaymentMethodAggregate.Specifications;
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
    private readonly IReadOnlyRepository<PaymentMethod> _paymentMethodRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<Material> _materialRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateContractCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;
    
    public UpdateContractCommandHandler(
        IRepository<Contract> contractRepository,
        IReadOnlyRepository<Product> productRepository,
        IReadOnlyRepository<ProductUnit> productUnitRepository,
        IReadOnlyRepository<PaymentMethod> paymentMethodRepository,
        IRepository<Customer> customerRepository,
        IReadOnlyRepository<Tax> taxRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateContractCommandHandler> logger,
        IMapper mapper,
        IEventBus eventBus,
        IRepository<Material> materialRepository)
    {
        _contractRepository = contractRepository;
        _productRepository = productRepository;
        _paymentMethodRepository = paymentMethodRepository;
        _productUnitRepository = productUnitRepository;
        _customerRepository = customerRepository;
        _taxRepository = taxRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _eventBus = eventBus;
        _materialRepository = materialRepository;
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
        // await UpdateItemsAsync(contract, request.Items);
        // await UpdateContractPaymentsAsync(contract, request.Payments);
        // await UpdateContractActionsAsync(contract, request.Actions);
        
        _contractRepository.Update(contract);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Updated contract {ContractId}", contract.Id);
        
        // Todo: update installation when the corresponding contract line is updated?
        
        // if (request.Installations != null && request.Installations.Any())
        // {
        //     await SendContractInstallationsUpdatedIntegrationEvent(request, contract);
        // }

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

    // private async Task UpdateItemsAsync(Contract contract, IList<ContractLineUpdateDto> items)
    // {
    //     if (!items.Any())
    //     {
    //         throw new ContractLineEmptyException();
    //     }
    //     
    //     var updatedLines = items.Where(x => x.Id.HasValue).ToArray();
    //     var deletedLines = contract.Items.ExceptBy(updatedLines.Select(x => x.Id), x => x.Id).ToArray();
    //     var newLines = items.Where(x => !x.Id.HasValue).ToArray();
    //     
    //     CheckDuplicateContractLine(updatedLines.Select(x => x.Id!.Value));
    //     
    //     var productIds = items.Select(x => x.ProductId).ToArray();
    //     var products = await _productRepository.FindListAsync(new ProductByIncludedIdsSpecification(productIds));
    //
    //     var productUnitIds = items.Select(x => x.UnitId).ToArray();
    //     var productUnits = await _productUnitRepository.FindListAsync(new ProductUnitByIncludedIdsSpecification(productUnitIds));
    //
    //     var taxIds = items.Where(x => x.TaxId.HasValue).Select(x => x.TaxId!.Value);
    //     var taxes = await _taxRepository.FindListAsync(new TaxByIncludedIdsSpecification(taxIds));
    //     
    //     foreach (var item in deletedLines)
    //     {
    //         contract.RemoveItem(item.Id);
    //     }
    //
    //     foreach (var item in updatedLines)
    //     {
    //         var product = GetProductById(item.ProductId, products);
    //         var productUnit = GetProductUnitById(item.UnitId, productUnits);
    //         
    //         contract.UpdateItem(
    //             item.Id!.Value, 
    //             item.ProductId,
    //             product.Name,
    //             item.UnitId,
    //             productUnit.Name,
    //             item.TaxId,
    //             GetTaxNameById(item.TaxId, taxes),
    //             GetTaxValueById(item.TaxId, taxes),
    //             item.Color,
    //             item.Quantity,
    //             item.Cost,
    //             item.SellPrice
    //         );
    //     }
    //     
    //     foreach (var item in newLines)
    //     {
    //         var product = GetProductById(item.ProductId, products);
    //         var productUnit = GetProductUnitById(item.UnitId, productUnits);
    //         
    //         contract.AddContractLine(
    //             item.ProductId, 
    //             product.Name, 
    //             item.UnitId,
    //             productUnit.Name,
    //             item.TaxId,
    //             GetTaxNameById(item.TaxId, taxes),
    //             GetTaxValueById(item.TaxId, taxes),
    //             item.Color,
    //             item.Quantity,
    //             item.Cost,
    //             item.SellPrice
    //         );
    //     }
    // }
    
    // private async Task UpdateContractPaymentsAsync(Contract contract, IList<ContractPaymentUpdateDto>? items)
    // {
    //     if (items == null || !items.Any())
    //     {
    //         return;
    //     }
    //     
    //     var newPayments = items.Where(x => !x.Id.HasValue).ToArray();
    //     var updatedPayments = items.Where(x => x.Id.HasValue && !x.IsDelete.GetValueOrDefault(false)).ToArray();
    //     var deletedPayments = items.Where(x => x.Id.HasValue && x.IsDelete.GetValueOrDefault(false)).ToArray();
    //     
    //     foreach (var item in deletedPayments)
    //     {
    //         contract.RemovePayment(item.Id!.Value);
    //     }
    //     
    //     var paymentMethodIds = items.Where(x => x.PaymentMethodId.HasValue).Select(x => x.PaymentMethodId!.Value).ToArray();
    //     var paymentMethods = await _paymentMethodRepository.FindListAsync(new PaymentMethodsByIncludedIdsSpecification(paymentMethodIds));
    //     
    //     foreach (var item in updatedPayments)
    //     {
    //         contract.UpdatePayment(
    //             item.Id!.Value,
    //             item.DatePaid,
    //             item.PaidAmount,
    //             item.Surcharge,
    //             item.Reference,
    //             item.Comments,
    //             item.PaymentMethodId,
    //             GetPaymentMethodNameById(item.PaymentMethodId, paymentMethods)
    //         );
    //     }
    //
    //     foreach (var payment in newPayments)
    //     {
    //         contract.AddPayment(
    //             payment.DatePaid,
    //             payment.PaidAmount,
    //             payment.Surcharge,
    //             payment.Reference,
    //             payment.Comments,
    //             payment.PaymentMethodId,
    //             GetPaymentMethodNameById(payment.PaymentMethodId, paymentMethods)
    //         );   
    //     }
    // }
    
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
    
    private double? GetTaxValueById(Guid? taxId, IEnumerable<Tax> taxes)
    {
        if (taxId.HasValue)
        {
            var tax = taxes.FirstOrDefault(x => x.Id == taxId);
            if (tax == null)
            {
                throw new TaxNotFoundException(taxId.Value);
            }

            return tax.Value;
        }

        return null;
    }
    
    private string? GetPaymentMethodNameById(Guid? paymentMethodId, IEnumerable<PaymentMethod> paymentMethods)
    {
        if (paymentMethodId.HasValue)
        {
            var paymentMethod = paymentMethods.FirstOrDefault(x => x.Id == paymentMethodId);
            if (paymentMethod == null)
            {
                throw new PaymentMethodNotFoundException(paymentMethodId.Value);
            }

            return paymentMethod.Name;
        }

        return null;
    }
    
    // private async Task UpdateContractActionsAsync(Contract contract, IList<ContractActionUpdateDto>? items)
    // {
    //     if (items == null || !items.Any())
    //     {
    //         return;
    //     }
    //     
    //     var newActions = items.Where(x => !x.Id.HasValue).ToArray();
    //     var updatedActions = items.Where(x => x.Id.HasValue && !x.IsDelete.GetValueOrDefault(false)).ToArray();
    //     var deletedActions = items.Where(x => x.Id.HasValue && x.IsDelete.GetValueOrDefault(false)).ToArray();
    //     
    //     foreach (var item in deletedActions)
    //     {
    //         contract.RemoveAction(item.Id!.Value);
    //     }
    //     
    //     // Todo: Validate employee
    //     
    //     foreach (var item in updatedActions)
    //     {
    //         contract.UpdateAction(
    //             item.Id!.Value,
    //             item.Name,
    //             item.Date,
    //             item.ActionByEmployeeId,
    //             item.Comment
    //         );
    //     }
    //     
    //     foreach (var item in newActions)
    //     {
    //         contract.AddAction(
    //             item.Name,
    //             item.Date,
    //             item.Comment,
    //             item.ActionByEmployeeId
    //         );    
    //     }
    // }
    
    private void MapContractLinesToInstallations(IList<InstallationUpdatedEventDto> installations, IList<ContractLine> contractLines)
    {
        foreach (var installation in installations)
        {
            if (installation.IsDelete) continue;
            installation.ContractLineId = contractLines.First(x => x.ProductId == installation.ProductId && x.Color == installation.Color).Id;
            installation.ProductName = contractLines.First(x => x.ProductId == installation.ProductId).ProductName;
        }
    }
    
    // Set materials name and units name for each installation
    private async Task SetInstallationItemsNameAndUnit(IList<InstallationUpdateDto>? installations)
    {
        if (installations == null || !installations.Any())
            return;
        
        foreach (var installation in installations)
        {
            // Set material name and unit name for each installation item
            var materialIds = installation.Items.Select(x => x.MaterialId).ToList();
            var materials = await _materialRepository.FindListAsync(new MaterialByIncludeIdsSpecification(materialIds)); 
            var notFoundMaterialIds = materialIds.Except(materials.Select(x => x.Id)).ToList();
            if (notFoundMaterialIds.Any())
                throw new MaterialNotFoundException(notFoundMaterialIds.First());
            installation.Items.ForEach(i => i.MaterialName = materials.First(x => x.Id == i.MaterialId).Name);
            
            var unitIds = installation.Items.Select(x => x.UnitId).ToList();
            var units = await _productUnitRepository.FindListAsync(new ProductUnitByIncludedIdsSpecification(unitIds));
            var notFoundUnitIds = unitIds.Except(units.Select(x => x.Id)).ToList();
            if (notFoundUnitIds.Any())
                throw new ProductUnitNotFoundException(notFoundUnitIds.First());
            installation.Items.ForEach(i => i.UnitName = units.First(x => x.Id == i.UnitId).Name);
        }
    }
    
    private bool IsAddressChanged(InstallationAddress address, InstallationAddressDto addressDto)
    {
        return address.FullAddress != addressDto.FullAddress ||
               address.City != addressDto.City ||
               address.State != addressDto.State ||
               address.PostalCode != addressDto.PostalCode;
    }
    
    // private async Task SendContractInstallationsUpdatedIntegrationEvent(UpdateContractCommand request,
    //                                                                     Contract contract)
    // {
    //     await SetInstallationItemsNameAndUnit(request.Installations);
    //     var installations = _mapper.Map<List<InstallationUpdatedEventDto>>(request.Installations);
    //     MapContractLinesToInstallations(installations, contract.Items);
    //     _eventBus.Publish(new ContractInstallationsUpdatedIntegrationEvent
    //     {
    //         ContractId = contract.Id,
    //         ContractNo = contract.No,
    //         CustomerId = contract.CustomerId,
    //         CustomerName = (await _customerRepository.GetByIdAsync(contract.CustomerId))!.Name,
    //         InstallationAddress = _mapper.Map<InstallationAddressEventDto>(request.InstallationAddress),
    //         IsAddressChanged = IsAddressChanged(contract.InstallationAddress, request.InstallationAddress),
    //         Installations = installations
    //     });
    // }
}