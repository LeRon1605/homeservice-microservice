using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts;

namespace Contracts.Application.Commands.Contracts.ConvertContractFromOrder;

public class ConvertContractFromOrderCommand : ContractConvertedFromOrderDto, ICommand<ContractDetailDto>
{
    public Guid OrderId { get; set; }
    
    public ConvertContractFromOrderCommand(Guid id, ContractConvertedFromOrderDto dto)
    {
        CustomerNote = dto.CustomerNote;
        SalePersonId = dto.SalePersonId;
        SupervisorId = dto.SupervisorId;
        CustomerServiceRepId = dto.CustomerServiceRepId;
        PurchaseOrderNo = dto.PurchaseOrderNo;
        InvoiceNo = dto.InvoiceNo;
        InvoiceDate = dto.InvoiceDate;
        EstimatedInstallationDate = dto.EstimatedInstallationDate;
        ActualInstallationDate = dto.ActualInstallationDate;
        InstallationAddress = dto.InstallationAddress;
        Items = dto.Items;
        Status = dto.Status;
        OrderId = id;
        Payments = dto.Payments;
        Actions = dto.Actions;
    }
}