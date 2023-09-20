using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;

namespace Products.Application.Commands.MaterialCommands.UpdateMaterial;

public class UpdateMaterialCommand : MaterialUpdateDto, ICommand<GetMaterialDto>
{
    public Guid Id { get; set; }
    
    public UpdateMaterialCommand(
        Guid id,
        MaterialUpdateDto dto)
    {
        Id = id;
        MaterialCode = dto.MaterialCode;
        Name = dto.Name;
        ProductTypeId = dto.ProductTypeId;
        SellUnitId = dto.SellUnitId;
        SellPrice = dto.SellPrice;
        Cost = dto.Cost;
        IsObsolete = dto.IsObsolete;
    }
}