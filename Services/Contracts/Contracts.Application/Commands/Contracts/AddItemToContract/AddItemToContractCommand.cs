using BuildingBlocks.Application.CQRS;
using Contracts.Application.Dtos.Contracts;
using Contracts.Application.Dtos.Contracts.ContractCreate;

namespace Contracts.Application.Commands.Contracts.AddItemToContract;

public class AddItemToContractCommand : ContractLineCreateDto, ICommand<ContractLineDto>
{
    public Guid ContractId { get; set; }
    
    public AddItemToContractCommand(Guid contractId, ContractLineCreateDto dto)
    {
        ContractId = contractId;
        
        ProductId = dto.ProductId;
        UnitId = dto.UnitId;
        TaxId = dto.TaxId;
        Color = dto.Color;
        Quantity = dto.Quantity;
        Cost = dto.Cost;
        SellPrice = dto.SellPrice;
    }
}