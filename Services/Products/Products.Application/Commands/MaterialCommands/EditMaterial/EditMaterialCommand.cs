using BuildingBlocks.Application.CQRS;
using Products.Application.Dtos;

namespace Products.Application.Commands.MaterialCommands.EditMaterial;

public class EditMaterialCommand : MaterialUpdateDto, ICommand<GetMaterialDto>
{
    public Guid Id { get; set; }
    
    public EditMaterialCommand(
        Guid id,
        string materialCode,
        string name,
        Guid productTypeId,
        Guid? sellUnitId,
        decimal? sellPrice,
        decimal? cost,
        bool isObsolete)
    {
        Id = id;
        MaterialCode = materialCode;
        Name = name;
        ProductTypeId = productTypeId;
        SellUnitId = sellUnitId;
        SellPrice = sellPrice;
        Cost = cost;
        IsObsolete = isObsolete;
    }
}