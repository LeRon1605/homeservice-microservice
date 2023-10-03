using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.ContractAggregate;
using Contracts.Domain.CustomerAggregate;
using Contracts.Domain.CustomerAggregate.Exceptions;
using Contracts.Domain.ProductAggregate;
using Contracts.Domain.ProductAggregate.Exceptions;
using Contracts.Domain.ProductAggregate.Specifications;
using Contracts.Domain.ProductUnitAggregate;
using Contracts.Domain.ProductUnitAggregate.Exceptions;
using Contracts.Domain.ProductUnitAggregate.Specifications;
using Microsoft.Extensions.Logging;

namespace Contracts.Application.Commands.Contracts.AddContract;

public class AddContractCommandHandler : ICommandHandler<AddContractCommand>
{
    private readonly IRepository<Contract> _contractRepository;
    private readonly IReadOnlyRepository<Product> _productRepository;
    private readonly IReadOnlyRepository<ProductUnit> _productUnitRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddContractCommandHandler> _logger;
    
    public AddContractCommandHandler(
        IRepository<Contract> contractRepository,
        IReadOnlyRepository<Product> productRepository,
        IReadOnlyRepository<ProductUnit> productUnitRepository,
        IRepository<Customer> customerRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddContractCommandHandler> logger)
    {
        _contractRepository = contractRepository;
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task Handle(AddContractCommand request, CancellationToken cancellationToken)
    {
        // Todo: Validate employee exist
        await CheckCustomerExistAsync(request.CustomerId);

        var contract = await CreateContractAsync(request);
        
        _contractRepository.Add(contract);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Contract added {ContractId}", contract.Id);
    }

    private async Task CheckCustomerExistAsync(Guid customerId)
    {
        if (!await _customerRepository.AnyAsync(customerId))
        {
            // Todo: create exception class
            throw new CustomerNotFoundException(customerId);
        }
    }

    private async Task<Contract> CreateContractAsync(AddContractCommand request)
    {
        var contract = new Contract(
            request.CustomerId, request.CustomerNote, request.SalePersonId,
            request.SupervisorId, request.CustomerServiceRepId, request.PurchaseOrderNo, request.InvoiceNo,
            request.InvoiceDate, request.EstimatedInstallationDate, request.ActualInstallationDate,
            request.InstallationAddress.FullAddress, request.InstallationAddress.City,
            request.InstallationAddress.State, request.InstallationAddress.PostalCode, request.Status);
        
        var productIds = request.Items.Select(x => x.ProductId).ToArray();
        var products = (await _productRepository.FindListAsync(new ProductByIncludedIdsSpecification(productIds))).ToList();

        var productUnitIds = request.Items.Select(x => x.UnitId).ToArray();
        var productUnits = (await _productUnitRepository.FindListAsync(new ProductUnitByIncludedIdsSpecification(productUnitIds))).ToList();
        
        foreach (var item in request.Items)
        {
            var product = products.FirstOrDefault(x => x.Id == item.ProductId);

            if (product == null)
            {
                throw new ProductNotFoundException(item.ProductId);
            }
            
            var productUnit = productUnits.FirstOrDefault(x => x.Id == item.UnitId);
            if (productUnit == null)
            {
                throw new ProductUnitNotFoundException(item.UnitId);
            }
            
            contract.AddContractLine(
                item.ProductId, 
                product.Name, 
                item.UnitId,
                productUnit.Name,
                item.Color,
                item.Quantity,
                item.Cost,
                item.SellPrice
            );
        }

        return contract;
    }
}