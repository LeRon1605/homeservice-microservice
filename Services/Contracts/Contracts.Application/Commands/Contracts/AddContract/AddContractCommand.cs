﻿using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Dtos.Contracts.ContractCreate;

namespace Contracts.Application.Commands.Contracts.AddContract;

public class AddContractCommand : ContractCreateDto, ICommand<ContractDetailDto>
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
        Payments = dto.Payments;
        Status = dto.Status;
        Installations = dto.Installations;
        Actions = dto.Actions;
        SoldAt = dto.SoldAt;
    }
}