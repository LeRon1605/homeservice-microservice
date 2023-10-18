using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Dtos.Contracts.ContractUpdate;

namespace Contracts.Application.Commands.Contracts.UpdateContractItem;

public class UpdateContractItemCommand : ContractLineUpdateDto, ICommand<ContractLineDto>
{
    public Guid ContractId { get; set; }
    public Guid ContractLineId { get; set; }

    public UpdateContractItemCommand(Guid contractId, Guid contractLineId, ContractLineUpdateDto dto)
    {
        ContractId = contractId;
        ContractLineId = contractLineId;
        ProductId = dto.ProductId;
        UnitId = dto.UnitId;
        TaxId = dto.TaxId;
        Color = dto.Color;
        Quantity = dto.Quantity;
        Cost = dto.Cost;
        SellPrice = dto.SellPrice;
    }
}