using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.EventBus.Interfaces;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.IntegrationEvents.Events.Contracts;
using Contracts.Application.IntegrationEvents.Events.Contracts.EventDtos;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.ContractAggregate.Exceptions;
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

namespace Contracts.Application.Commands.Contracts.AddContract;

public class AddContractCommandHandler : ICommandHandler<AddContractCommand, ContractDetailDto>
{
    private readonly IRepository<Contract> _contractRepository;
    private readonly IReadOnlyRepository<Product> _productRepository;
    private readonly IReadOnlyRepository<Material> _materialRepository;
    private readonly IReadOnlyRepository<ProductUnit> _productUnitRepository;
    private readonly IReadOnlyRepository<PaymentMethod> _paymentMethodRepository;
    private readonly IReadOnlyRepository<Tax> _taxRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddContractCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;
    
    public AddContractCommandHandler(
        IRepository<Contract> contractRepository,
        IReadOnlyRepository<Product> productRepository,
        IReadOnlyRepository<ProductUnit> productUnitRepository,
        IReadOnlyRepository<PaymentMethod> paymentMethodRepository,
        IRepository<Customer> customerRepository,
        IReadOnlyRepository<Tax> taxRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddContractCommandHandler> logger,
        IMapper mapper,
        IEventBus eventBus,
        IReadOnlyRepository<Material> materialRepository)
    {
        _contractRepository = contractRepository;
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;
        _customerRepository = customerRepository;
        _paymentMethodRepository = paymentMethodRepository;
        _taxRepository = taxRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _eventBus = eventBus;
        _materialRepository = materialRepository;
    }
    
    public async Task<ContractDetailDto> Handle(AddContractCommand request, CancellationToken cancellationToken)
    {
        // Todo: Validate employee exist (salesperson, supervisor, customer service rep and installer)
        await CheckCustomerExistAsync(request.CustomerId);
        await SetInstallationItemsNameAndUnit(request.Installations);

        var contract = new Contract(
            request.CustomerId, request.CustomerNote, request.SalePersonId,
            request.SupervisorId, request.CustomerServiceRepId, request.PurchaseOrderNo, request.InvoiceNo,
            request.InvoiceDate, request.EstimatedInstallationDate, request.ActualInstallationDate,
            request.InstallationAddress.FullAddress, request.InstallationAddress.City,
            request.InstallationAddress.State, request.InstallationAddress.PostalCode, request.SoldAt, request.Status);

        await AddContractLineAsync(contract, request);
        await AddPaymentsAsync(contract, request);
        await AddActionsAsync(contract, request);
        
        _contractRepository.Add(contract);
        await _unitOfWork.SaveChangesAsync();
        
        var installations = _mapper.Map<List<InstallationEventDto>>(request.Installations);
        // Each installation differs by its product and color, so I use them to find the corresponding contract line id
        MapContractLinesToInstallations(installations, contract.Items);
        
        _eventBus.Publish(new ContractCreatedIntegrationEvent
        {
            ContractId = contract.Id,
            ContractNo = contract.No,
            InstallationAddress = _mapper.Map<InstallationAddressEventDto>(request.InstallationAddress),
            CustomerId = contract.CustomerId,
            CustomerName = (await _customerRepository.GetByIdAsync(contract.CustomerId))!.Name,
            Installations = installations
        });
        
        _logger.LogInformation("Contract added {ContractId}", contract.Id);

        return _mapper.Map<ContractDetailDto>(contract);
    }

    private async Task AddContractLineAsync(Contract contract, AddContractCommand request)
    {
        if (!request.Items.Any())
        {
            throw new ContractLineEmptyException();
        }
        
        var productIds = request.Items.Select(x => x.ProductId).ToArray();
        var products = await _productRepository.FindListAsync(new ProductByIncludedIdsSpecification(productIds));

        var productUnitIds = request.Items.Select(x => x.UnitId).ToArray();
        var productUnits = await _productUnitRepository.FindListAsync(new ProductUnitByIncludedIdsSpecification(productUnitIds));

        var taxIds = request.Items.Where(x => x.TaxId.HasValue).Select(x => x.TaxId!.Value);
        var taxes = await _taxRepository.FindListAsync(new TaxByIncludedIdsSpecification(taxIds));
        
        foreach (var item in request.Items)
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

    private async Task AddPaymentsAsync(Contract contract, AddContractCommand request)
    {
        if (request.Payments == null || !request.Payments.Any())
        {
            return;
        }
        
        var paymentMethodIds = request.Payments.Where(x => x.PaymentMethodId.HasValue).Select(x => x.PaymentMethodId!.Value).ToArray();
        var paymentMethods =await _paymentMethodRepository.FindListAsync(new PaymentMethodsByIncludedIdsSpecification(paymentMethodIds));
        
        foreach (var payment in request.Payments)
        {
            contract.AddPayment(
                payment.DatePaid,
                payment.PaidAmount,
                payment.Surcharge,
                payment.Reference,
                payment.Comments,
                payment.PaymentMethodId,
                GetPaymentMethodNameById(payment.PaymentMethodId, paymentMethods)
            );    
        }
    }
    
    private async Task AddActionsAsync(Contract contract, AddContractCommand request)
    {
        if (request.Actions == null || !request.Actions.Any())
        {
            return;
        }
        
        // Todo: Validate employee
        
        foreach (var action in request.Actions)
        {
            contract.AddAction(
                action.Name,
                action.Date,
                action.Comment,
                action.ActionByEmployeeId
            );    
        }
    }
    
    private async Task CheckCustomerExistAsync(Guid customerId)
    {
        if (!await _customerRepository.AnyAsync(customerId))
        {
            throw new CustomerNotFoundException(customerId);
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

    private void MapContractLinesToInstallations(IList<InstallationEventDto> installations, IList<ContractLine> contractLines)
    {
        foreach (var installation in installations)
        {
            installation.ContractLineId = contractLines.First(x => x.ProductId == installation.ProductId && x.Color == installation.Color).Id;
            installation.ProductName = contractLines.First(x => x.ProductId == installation.ProductId).ProductName;
        }
    }

    // Set materials name and units name for each installation
    private async Task SetInstallationItemsNameAndUnit(IList<InstallationCreateDto>? installations)
    {
        if (installations == null || !installations.Any())
            return;
        
        foreach (var installation in installations)
        {
            // Set material name and unit name for each installation item
            var materialIds = installation.Items.Select(x => x.MaterialId).ToList();
            var materials = await _materialRepository.FindListAsync(new MaterialByIncludeIdsSpecification(materialIds)); 
            if (materials.Count != materialIds.Count)
            {
                var notFoundMaterialIds = materialIds.Except(materials.Select(x => x.Id));
                throw new MaterialNotFoundException(notFoundMaterialIds.First());
            }
            installation.Items.ForEach(i => i.MaterialName = materials.First(x => x.Id == i.MaterialId).Name);
            
            var unitIds = installation.Items.Select(x => x.UnitId).ToList();
            var units = await _productUnitRepository.FindListAsync(new ProductUnitByIncludedIdsSpecification(unitIds));
            if (units.Count != unitIds.Count)
            {
                var notFoundUnitIds = unitIds.Except(units.Select(x => x.Id));
                throw new ProductUnitNotFoundException(notFoundUnitIds.First());
            }
            installation.Items.ForEach(i => i.UnitName = units.First(x => x.Id == i.UnitId).Name);
        }
    }
}