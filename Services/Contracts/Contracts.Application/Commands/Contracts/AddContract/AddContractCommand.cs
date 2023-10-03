using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts;

namespace Contracts.Application.Commands.Contracts.AddContract;

public class AddContractCommand : ContractCreateDto, ICommand
{
    public AddContractCommand(ContractCreateDto dto)
    {
        CustomerId = dto.CustomerId;
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
    }
}