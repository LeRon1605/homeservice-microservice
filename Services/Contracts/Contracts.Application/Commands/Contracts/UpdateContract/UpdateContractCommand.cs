using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts;

namespace Contracts.Application.Commands.Contracts.UpdateContract;

public class UpdateContractCommand : ContractUpdateDto, ICommand<ContractDetailDto>
{
    public Guid Id { get; set; }

    public UpdateContractCommand(Guid id, ContractUpdateDto dto)
    {
        Id = id;
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
        Payments = dto.Payments;
        Status = dto.Status;
        Actions = dto.Actions;
    }
}