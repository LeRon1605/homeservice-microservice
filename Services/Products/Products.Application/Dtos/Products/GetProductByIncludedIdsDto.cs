using System.ComponentModel.DataAnnotations;

namespace Products.Application.Dtos.Products;

public class GetProductByIncludedIdsDto
{
    [MinLength(1)]
    public IEnumerable<Guid> Ids { get; set; }
}