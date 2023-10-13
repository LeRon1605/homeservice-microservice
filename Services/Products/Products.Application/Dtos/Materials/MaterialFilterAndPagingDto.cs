using BuildingBlocks.Application.Dtos;

namespace Products.Application.Dtos.Materials;

public class MaterialFilterAndPagingDto : PagingParameters
{
    public bool? IsObsolete { get; set; }
    public Guid? TypeId { get; set; }
}