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
using Contracts.Domain.EmployeeAggregate;
using Contracts.Domain.EmployeeAggregate.Exceptions;
using Contracts.Domain.EmployeeAggregate.Specifications;
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
    private readonly IReadOnlyRepository<ProductUnit> _productUnitRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<Material> _materialRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateContractCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<Employee> _employeeRepository;
    
    public UpdateContractCommandHandler(
        IRepository<Contract> contractRepository,
        IReadOnlyRepository<ProductUnit> productUnitRepository,
        IRepository<Customer> customerRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateContractCommandHandler> logger,
        IMapper mapper,
        IReadOnlyRepository<Employee> employeeRepository,
        IRepository<Material> materialRepository)
    {
        _contractRepository = contractRepository;
        _productUnitRepository = productUnitRepository;
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _employeeRepository = employeeRepository;
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
        await UpdateSalePersonAsync(contract, request.SalePersonId);    
        await UpdateSupervisorAsync(contract, request.SupervisorId);
        await UpdateCustomerServiceRepAsync(contract, request.CustomerServiceRepId);
        
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

    private async Task UpdateSalePersonAsync(Contract contract, Guid salePersonId)
    {
        if (contract.SalePersonId != salePersonId)
        {
            await CheckSalePersonAsync(salePersonId);
            contract.UpdateSalePerson(salePersonId);
        }
    }
    
    private async Task UpdateSupervisorAsync(Contract contract, Guid? supervisorId)
    {
        if (supervisorId.HasValue && contract.SupervisorId != supervisorId.Value)
        {
            await CheckSupervisorAsync(supervisorId);
            contract.UpdateSalePerson(supervisorId.Value);
        }
    }
    
    private async Task UpdateCustomerServiceRepAsync(Contract contract, Guid? customerServiceRepId)
    {
        if (customerServiceRepId.HasValue && contract.SalePersonId != customerServiceRepId.Value)
        {
            await CheckCustomerServiceRepAsync(customerServiceRepId);
            contract.UpdateSalePerson(customerServiceRepId.Value);
        }
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
    
    private async Task CheckSalePersonAsync(Guid salePersonId)
    {
        if (!await _employeeRepository.AnyAsync(new IsSalePersonEmployeeSpecification(salePersonId)))
        {
            throw new SalePersonNotFoundException(salePersonId);
        }
    }

    private async Task CheckSupervisorAsync(Guid? supervisorId)
    {
        if (supervisorId.HasValue && !await _employeeRepository.AnyAsync(new IsSupervisorEmployeeSpecification(supervisorId.Value)))
        {
            throw new SupervisorNotFoundException(supervisorId.Value);
        }
    }

    private async Task CheckCustomerServiceRepAsync(Guid? customerServiceRepId)
    {
        if (customerServiceRepId.HasValue && !await _employeeRepository.AnyAsync(new IsCustomerServiceEmployeeSpecification(customerServiceRepId.Value)))
        {
            throw new SupervisorNotFoundException(customerServiceRepId.Value);
        }
    }
}